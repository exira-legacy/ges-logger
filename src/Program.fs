open System.Diagnostics
open System.Reflection
open System.IO
open FSharp.Configuration
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

let stop _ =
    // TODO: Disconnect from GES
    true

let start hostControl =
    let logger = LoggerConfiguration().Destructure.JsonNetTypes()

    let logger =
        if loggerConfig.Logger.Sinks.Console.Enabled then
            logger.WriteTo.ColoredConsole()
        else logger

    let logger =
        if loggerConfig.Logger.Sinks.RollingFile.Enabled then
            logger.WriteTo.RollingFile(loggerConfig.Logger.Sinks.RollingFile.PathFormat)
        else logger

    let log = logger.CreateLogger()

    // TODO: Connect to GES and send all events to Serilog
    true

[<EntryPoint>]
let main argv =
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
    |> run
