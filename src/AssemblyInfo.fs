namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("ges-logger")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore.Logger")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore.Logger is a Topshelf service to log EventStore events using Serilog")>]
[<assembly: AssemblyVersionAttribute("0.3.3")>]
[<assembly: AssemblyFileVersionAttribute("0.3.3")>]
[<assembly: AssemblyMetadataAttribute("githash","eaa48c387df2878ad7f8adcceffdac3beecf6c11")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.3.3"
