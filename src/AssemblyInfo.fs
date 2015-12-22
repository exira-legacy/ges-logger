namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("ges-logger")>]
[<assembly: AssemblyProductAttribute("Exira.EventStore.Logger")>]
[<assembly: AssemblyDescriptionAttribute("Exira.EventStore.Logger is a Topshelf service to log EventStore events using Serilog")>]
[<assembly: AssemblyVersionAttribute("0.1.0")>]
[<assembly: AssemblyFileVersionAttribute("0.1.0")>]
[<assembly: AssemblyMetadataAttribute("githash","6bbba72110be6c8c09d598df906bfd51bd2bd3cc")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.0"
