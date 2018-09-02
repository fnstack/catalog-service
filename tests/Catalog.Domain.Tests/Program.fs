open Expecto

[<EntryPoint>]
let main args =
  Tests.runTestsInAssembly defaultConfig args |> ignore

  System.Console.ReadKey() |> ignore

  0
