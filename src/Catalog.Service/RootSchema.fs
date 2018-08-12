namespace Catalog.API

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types
open Catalog.API.ProductBrandSchema
open Catalog.API.ProductCategorySchema

type Root =
    { ClientId: System.Guid }

module Schema =

    let mutable brands = [
                   {Id = "681f3f83-2580-4c54-ac0a-f18dd1b0d73a" |> System.Guid; Name = "Apple"; Description = "Un des leader GAFA"}
                   {Id = "cc6b592e-b344-4daa-85d9-85ff501dc59c" |> System.Guid; Name = "Nokia"; Description = ""}
                   {Id = "c79fdfc5-cfa8-43ac-8617-9df4b94c4cd1" |> System.Guid; Name = "Samsung"; Description = ""}
                 ]
    let mutable categories = [
                   {Id = "681f3f83-2580-4c54-ac0a-f18dd1b0d73b" |> System.Guid; Name = "Music";ParentId=None; Description = None}
                   {Id = "cc6b592e-b344-4daa-85d9-85ff501dc59c" |> System.Guid; Name = "Sport";ParentId=None; Description = None}
                   {Id = "c79fdfc5-cfa8-43ac-8617-9df4b94c4cd1" |> System.Guid; Name = "MultiMedia";ParentId=None; Description = None}
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

    let Mutation =
        Define.Object<Root>(
            name = "Mutation",
            fields = [
                Define.AsyncField(
                            "createProductBrand",
                            ID,
                            "Create a new product brand",
                            [ Define.Input("input", CreateProductBrandInput) ],
                            fun ctx _ -> async {
                                let input = ctx.Arg<CreateProductBrandInput>("input")

                                let id = System.Guid.NewGuid()

                                match brands |> List.exists (fun brand -> brand.Name.ToLower() = input.Name.ToLower()) with
                                | false ->
                                    brands <- brands |> List.append [{Id = id; Name = input.Name; Description = ""}]
                                | true ->
                                    failwith (sprintf "A product brand with name %s already exists" input.Name)

                                return id
                              })

                Define.AsyncField(
                            "createProductCategory",
                            ID,
                            "Create a new product category",
                            [ Define.Input("input", CreateProductCategoryInput) ],
                            fun ctx _ -> async {
                                let input = ctx.Arg<CreateProductCategoryInput>("input")

                                let id = System.Guid.NewGuid()

                                let checkDescription = fun value -> match value |> System.String.IsNullOrWhiteSpace with true -> None | false -> value |> Some



                                match categories |> List.exists (fun category -> category.Name.ToLower() = input.Name.ToLower()) with
                                | false ->
                                    categories <- categories |> List.append [{Id = id; Name = input.Name; Description = input.Description |> checkDescription; ParentId = input.ParentId |> checkDescription }]
                                | true ->
                                    failwith (sprintf "A product category with name %s already exists" input.Name)

                                return id
                              })
            ])

    let executor = Schema(query = Query, mutation = Mutation) :> ISchema<Root> |> Executor
    let root = { ClientId = System.Guid.NewGuid() }