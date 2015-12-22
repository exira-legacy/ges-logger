# ges-logger

[Topshelf](http://topshelf-project.com/) service to log [EventStore](https://geteventstore.com/) events using Serilog.

## Usage

To install do the following:

  * Download a release from the [Releases page](https://github.com/exira/ges-logger/releases) and unzip somewhere, for example ```C:\ges-logger\```

  * Edit ```Logger.yaml``` using:
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

  * Run the ```Install.ps1``` script with the same servicename you configured in ```Logger.yaml```, for example: ```powershell ./Install.ps1 -servicename ges-logger```

  * Instead you can also simply run one of the following:
    * ```ges-logger.exe install```
    * ```ges-logger.exe start```
    * ```ges-logger.exe stop```
    * ```ges-logger.exe uninstall```

  * Enjoy your logged events

## Example Output

```
λ ges-logger.exe install
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
```

```
λ ges-logger.exe start
Configuration Result:
[Success] Name ges-logger
[Success] Description Log Event Store events
[Success] ServiceName ges-logger
Topshelf v3.2.150.0, .NET Framework v4.0.30319.42000
The ges-logger service was started.
```

```
λ ges-logger.exe stop
Configuration Result:
[Success] Name ges-logger
[Success] Description Log Event Store events
[Success] ServiceName ges-logger
Topshelf v3.2.150.0, .NET Framework v4.0.30319.42000
The ges-logger service was stopped.
```

```
λ ges-logger.exe uninstall
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
```

## Cloning

```git clone git@github.com:exira/ges-logger.git -c core.autocrlf=input```

## Copyright

Copyright © 2015 Cumps Consulting / Exira and contributors.

## License

ges-logger is licensed under [BSD (3-Clause)](http://choosealicense.com/licenses/bsd-3-clause/ "Read more about the BSD (3-Clause) License"). Refer to [LICENSE](https://github.com/exira/ges-logger/blob/master/LICENSE.txt) for more information.
