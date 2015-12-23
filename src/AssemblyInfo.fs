namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("ges-logger")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore.Logger")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore.Logger is a Topshelf service to log EventStore events using Serilog")>]
[<assembly: AssemblyVersionAttribute("0.1.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0")>]
[<assembly: AssemblyMetadataAttribute("githash","32804a1878244d2d587d34305cb919023c8dcf00")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0"
