namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("ges-logger")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore.Logger")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore.Logger is a Topshelf service to log EventStore events using Serilog")>]
[<assembly: AssemblyVersionAttribute("0.3.2")>]
[<assembly: AssemblyFileVersionAttribute("0.3.2")>]
[<assembly: AssemblyMetadataAttribute("githash","13041dd253ca9a460fdbab379b635cb6ac187e45")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.3.2"
