namespace Catalog.API

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types
open FSharp.Data.GraphQL.Server.Middlewares
open Catalog.API.ProductBrandSchema
open Catalog.API.ProductCategorySchema
open System.ComponentModel

type Root =
    { ClientId: System.Guid }

module Schema =

    //let SchemaConfig = SchemaConfig.Default

    let brands = [
                   {Id = "681f3f83-2580-4c54-ac0a-f18dd1b0d73a" |> System.Guid; Name = "Apple"}
                   {Id = "cc6b592e-b344-4daa-85d9-85ff501dc59c" |> System.Guid; Name = "Nokia"} 
                   {Id = "c79fdfc5-cfa8-43ac-8617-9df4b94c4cd1" |> System.Guid; Name = "Samsung"}
                 ]
    let categories = [
                   {Id = "681f3f83-2580-4c54-ac0a-f18dd1b0d73b" |> System.Guid; Name = "Music";ParentId="681f3f83-2580-4c54-ac0a-f18dd1b0d73a"|> System.Guid;}
                   {Id = "cc6b592e-b344-4daa-85d9-85ff501dc59c" |> System.Guid; Name = "Sport";ParentId="681f3f83-2580-4c54-ac0a-f18dd1b0d73a"|> System.Guid;} 
                   {Id = "c79fdfc5-cfa8-43ac-8617-9df4b94c4cd1" |> System.Guid; Name = "MultiMedia";ParentId="681f3f83-2580-4c54-ac0a-f18dd1b0d73a"|> System.Guid;}
                 ]

    let Query =
            Define.Object<Root>(
                name = "Query",
                fields = [
                    Define.AsyncField(
                                "productBrands",
                                ListOf ProductBrandType,
                                "Gets product brands",
                                [
                                ],
                                fun ctx _ -> async {
                                    return brands
                                })

                    Define.AsyncField(
                                "productBrand",
                                ProductBrandType,
                                "Gets product brand by id",
                                [
                                    Define.Input("id", String)
                                ],
                                fun ctx _ -> async {
                                    let id = ctx.Arg("id").ToString() |> System.Guid

                                    let brand = brands |> Seq.filter (fun t -> t.Id = id) |> Seq.head

                                    return brand
                                })           
                    Define.AsyncField(
                                "productCategory",
                                ProductCategoryType,
                                "Gets product Categories by id",
                                [
                                    Define.Input("id", String)
                                ],
                                fun ctx _ -> async {
                                    let id = ctx.Arg("id").ToString() |> System.Guid

                                    let category = categories |> Seq.filter (fun t -> t.Id = id) |> Seq.head 

                                    return category
                                })



                    Define.AsyncField(
                               "productCategories",
                               ListOf ProductCategoryType,
                               "Gets product categories",
                               [
                               ],
                               fun ctx _ -> async {
                                   return categories
                               })
                ])

    let executor = Schema(query = Query) :> ISchema<Root> |> Executor
    let root = { ClientId = System.Guid.NewGuid() }