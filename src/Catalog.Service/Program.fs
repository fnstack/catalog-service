namespace Catalog.API

open System
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting

module Program =

    let exitCode = 0

    let BuildWebHost args =
        WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseUrls("http://localhost:8705")
            .Build()

    [<EntryPoint>]
    let main args =
        Console.Title <- "Catalog Service"
        
        BuildWebHost(args).Run()
        exitCode
