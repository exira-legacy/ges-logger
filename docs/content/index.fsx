(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
ges-logger [![NuGet Status](http://img.shields.io/nuget/v/Exira.EventStore.Logger.svg?style=flat)](https://www.nuget.org/packages/Exira.EventStore.Logger/)
======================


[Topshelf](http://topshelf-project.com/) service to log [EventStore](https://geteventstore.com/) events using [Serilog](http://serilog.net/).

### Usage

To install do the following:

  * Download a release from the [Releases page](https://github.com/exira/ges-logger/releases) and unzip somewhere, for example `C:\ges-logger\`

  * Edit `Logger.yaml` using:
    * Service:
      * ServiceName: `ges-logger`, the name which will show up in Services.
      * Description: `Log Event Store event`, the description used in Services.

    * EventStore:
      * ConnectionString: `ConnectTo=tcp://admin:changeit@127.0.0.1:8003`, the EventStore connection string to grab all events from.

    * Sinks:
      * Console:
        * Enabled: `true`, turn on ColoredConsole logger.
      * RollingFile:
        * Enabled: `true`, turn on ColoredConsole logger.
      * Seq:
        * Enabled: `true`, turn on ColoredConsole logger.

  * Run the `Install.ps1` script with the same servicename you configured in `Logger.yaml`, for example: `powershell ./Install.ps1 -servicename ges-logger`

  * Instead you can also simply run one of the following:
    * `ges-logger.exe install`
    * `ges-logger.exe start`
    * `ges-logger.exe stop`
    * `ges-logger.exe uninstall`

  * Enjoy your logged events

### Example Output

<pre><code>λ ges-logger.exe install
Configuration Result:
[Success] Name ges-logger
[Success] Description Log Event Store events
[Success] ServiceName ges-logger
Topshelf v3.2.150.0, .NET Framework v4.0.30319.42000
Running a transacted installation.
Beginning the Install phase of the installation.
Installing ges-logger service
Installing service ges-logger...
Service ges has been successfully installed.
The Install phase completed successfully, and the Commit phase is beginning.
The Commit phase completed successfully.
The transacted install has completed.
</code></pre>

<pre><code>λ ges-logger.exe start
Configuration Result:
[Success] Name ges-logger
[Success] Description Log Event Store events
[Success] ServiceName ges-logger
Topshelf v3.2.150.0, .NET Framework v4.0.30319.42000
The ges-logger service was started.
</code></pre>

<pre><code>λ ges-logger.exe stop
Configuration Result:
[Success] Name ges-logger
[Success] Description Log Event Store events
[Success] ServiceName ges-logger
Topshelf v3.2.150.0, .NET Framework v4.0.30319.42000
The ges-logger service was stopped.
</code></pre>

<pre><code>λ ges-logger.exe uninstall
Configuration Result:
[Success] Name ges-logger
[Success] Description Log Event Store events
[Success] ServiceName ges-logger
Topshelf v3.2.150.0, .NET Framework v4.0.30319.42000

The uninstall is beginning.
Uninstalling ges-logger service
Removing EventLog source ges.
Service ges-logger is being removed from the system...
Service ges-logger was successfully removed from the system.
The uninstall has completed.
</code></pre>

### Cloning

`git clone git@github.com:exira/ges-logger.git -c core.autocrlf=input`

### Contributing and copyright

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork
the project and submit pull requests. You might also want to read the
[library design notes][readme] to understand how it works.

For more information see the [License file][license] in the GitHub repository.

  [content]: https://github.com/exira/ges-logger/tree/master/docs/content
  [gh]: https://github.com/exira/ges-logger
  [issues]: https://github.com/exira/ges-logger/issues
  [readme]: https://github.com/exira/ges-logger/blob/master/README.md
  [license]: https://github.com/exira/ges-logger/blob/master/LICENSE.txt
*)
