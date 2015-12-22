open System.Diagnostics
open System.Reflection
open System.IO
open FSharp.Configuration
open Topshelf
open Time

let executablePath = Assembly.GetEntryAssembly().Location |> Path.GetDirectoryName
let configPath = Path.Combine(executablePath, "Logger.yaml")

type LoggerConfig = YamlConfig<"Logger.yaml">
let loggerConfig = LoggerConfig()
loggerConfig.Load configPath

let stop _ =
    // TODO: Disconnect from GES
    true

let start hostControl =
    // TODO: Connect to GES, wire up Serilog and send all events to Serilog
    true

[<EntryPoint>]
let main argv =
    Service.Default
    |> run_as_local_system
    |> start_auto
    |> enable_shutdown
    |> with_recovery (ServiceRecovery.Default |> restart (min loggerConfig.Runner.RestartIntervalInMinutes))
    |> with_start start
    |> with_stop stop
    |> description loggerConfig.Runner.Description
    |> display_name loggerConfig.Runner.ServiceName
    |> service_name loggerConfig.Runner.ServiceName
    |> run
