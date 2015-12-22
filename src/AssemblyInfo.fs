namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("ges-logger")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore.Logger")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore.Logger is a Topshelf service to log EventStore events using Serilog")>]
[<assembly: AssemblyVersionAttribute("0.1.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0")>]
[<assembly: AssemblyMetadataAttribute("githash","8d8f33636fab3f434a03634bf49af791263d8f77")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0"
