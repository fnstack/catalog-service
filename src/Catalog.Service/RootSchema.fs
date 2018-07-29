namespace Catalog.API

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types
open FSharp.Data.GraphQL.Server.Middlewares
open Microsoft.Extensions.DependencyInjection

type Root =
    { ClientId: System.Guid }

module Schema =

    //let SchemaConfig = SchemaConfig.Default

    let Query =
            Define.Object<Root>(
                name = "Query",
                fields = [
                    Define.AsyncField(
                                "SmartPhones",
                                ListOf String,
                                "Gets smart phone list",
                                [
                                ],
                                fun ctx _ -> async {

                                    let chats = [|"IPhone X"; "Samsung Galaxy S5"; "Nokia Lumia 650"|]

                                    return chats
                                })
                ])

    let executor = Schema(query = Query) :> ISchema<Root> |> Executor
    let root = { ClientId = System.Guid.NewGuid() }