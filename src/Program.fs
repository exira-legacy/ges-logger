open System.Reflection
open System.Text
open System.IO
open FSharp.Configuration
open Exira.EventStore
open Exira.EventStore.EventStore
open EventStore.ClientAPI
open Topshelf
open Time
open Serilog
open Destructurama
open Newtonsoft.Json

let executablePath = Assembly.GetEntryAssembly().Location |> Path.GetDirectoryName
let configPath = Path.Combine(executablePath, "Logger.yaml")

type LoggerConfig = YamlConfig<"Logger.yaml">
let loggerConfig = LoggerConfig()
loggerConfig.Load configPath

let private checkpointStreamName = sprintf "%s-checkpoint" loggerConfig.Logger.Service.ServiceName
let private checkpointStream = checkpointStreamName |> StreamId

let mutable (es: IEventStoreConnection option) = None

let log =
    let logger =
        LoggerConfiguration()
            .Destructure.JsonNetTypes()

    let logger =
        loggerConfig.Logger.Properties
        |> Seq.fold (fun (logger: LoggerConfiguration) p -> logger.Enrich.WithProperty(p.key, p.value)) logger

    let logger =
        if loggerConfig.Logger.Sinks.Console.Enabled then
            logger.WriteTo.ColoredConsole()
        else logger

    let logger =
        if loggerConfig.Logger.Sinks.RollingFile.Enabled then
            logger.WriteTo.RollingFile(loggerConfig.Logger.Sinks.RollingFile.PathFormat)
        else logger

    let logger =
        if loggerConfig.Logger.Sinks.Seq.Enabled then
            logger.WriteTo.Seq(loggerConfig.Logger.Sinks.Seq.Url.ToString(), apiKey = loggerConfig.Logger.Sinks.Seq.ApiKey)
        else logger

    logger.CreateLogger()

let logEvent esConnection (resolvedEvent: ResolvedEvent) =
    let json = Encoding.UTF8.GetString resolvedEvent.OriginalEvent.Data

    log.Information("{stream}@{number} {type} {@event}",
        resolvedEvent.OriginalStreamId,
        resolvedEvent.OriginalEventNumber,
        resolvedEvent.OriginalEvent.EventType,
        JsonConvert.DeserializeObject json)

    let result = storeCheckpoint esConnection checkpointStream resolvedEvent.OriginalPosition.Value |> Async.Catch |> Async.RunSynchronously
    match result with
    | Choice1Of2 _ -> ()
    | Choice2Of2 ex ->
        match ex with
        | :? System.AggregateException as aex ->
            match aex.InnerException with
            | :? System.ObjectDisposedException -> () // The connection has already been closed, but there are still events incoming
            | _ -> raise aex.InnerException
        | _ -> raise ex

let private eventAppeared esConnection = fun _ (resolvedEvent: ResolvedEvent) ->
    if resolvedEvent.OriginalStreamId.StartsWith "$" then ()
    else logEvent esConnection resolvedEvent

let private subscribe esConnection = fun reconnect ->
    let lastPosition = getCheckpoint esConnection checkpointStream |> Async.RunSynchronously
    subscribeToAllFrom esConnection lastPosition true (eventAppeared esConnection) ignore reconnect

let rec private subscriptionDropped esConnection = fun _ reason ex  ->
    printfn "Subscription Dropped: %O - %O" reason ex
    if reason = SubscriptionDropReason.ConnectionClosed then ()
    else subscribe esConnection (subscriptionDropped esConnection) |> ignore

let stop _ =
    match es with
        | None -> true
        | Some esConnection ->
            esConnection.Close()
            es <- None
            true

let start _ =
    let esConnection = connect loggerConfig.Logger.EventStore.ConnectionString |> Async.RunSynchronously
    initalizeCheckpoint esConnection checkpointStream |> Async.RunSynchronously
    subscribe esConnection (subscriptionDropped esConnection) |> ignore
    es <- Some esConnection
    true

[<EntryPoint>]
let main _ =
    let service =
        Service.Default
        |> run_as_local_system
        |> start_auto
        |> enable_shutdown
        |> with_recovery (ServiceRecovery.Default |> restart (min loggerConfig.Logger.Service.RestartIntervalInMinutes))
        |> with_start start
        |> with_stop stop
        |> description loggerConfig.Logger.Service.Description
        |> display_name loggerConfig.Logger.Service.ServiceName
        |> service_name loggerConfig.Logger.Service.ServiceName

    let service =
        if loggerConfig.Logger.Service.HasDependencies then
            loggerConfig.Logger.Service.DependsOn
            |> Seq.fold (fun s dependency -> s |> depends_on dependency) service
        else service

    service
    |> run
