namespace Catalog.API

open FSharp.Data.GraphQL
open FSharp.Data.GraphQL.Types
open Catalog.API.ProductBrandSchema
open Catalog.API.ProductCategorySchema
open Catalog.API.ProductSchema

type Root =
    { ClientId: System.Guid }

module Schema =
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

                    Define.AsyncField(
                               "products",
                               ListOf ProductType,
                               "Gets products",
                               [
                               ],
                               fun ctx _ -> async {
                               return products
                               })

                    Define.AsyncField(
                                "product",
                                ProductType,
                                "Gets product by id",
                                [
                                    Define.Input("id", String)
                                ],
                                fun ctx _ -> async {
                                    let id = ctx.Arg("id").ToString() |> System.Guid

                                    let product = products |> Seq.filter (fun t -> t.Id = id) |> Seq.head

                                    return product
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
                                    brands <- brands |> List.append [{Id = id; Name = input.Name; Description = None}]
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

                                match categories |> List.exists (fun category -> category.Name.ToLower() = input.Name.ToLower()) with
                                | false ->
                                    categories <- categories |> List.append [{Id = id; Name = input.Name; Description = input.Description; ParentId = input.ParentId }]
                                | true ->
                                    failwith (sprintf "A product category with name %s already exists" input.Name)

                                return id
                              })

                Define.AsyncField(
                            "createProduct",
                            ID,
                            "Create a new product",
                            [ Define.Input("input", CreateProductInput) ],
                            fun ctx _ -> async {
                                let input = ctx.Arg<CreateProductInput>("input")

                                let id = System.Guid.NewGuid()

                                match products |> List.exists (fun brand -> brand.Name.ToLower() = input.Name.ToLower()) with
                                | false ->
                                    products <- products |> List.append [{Id = id; Name = input.Name; BrandId = input.BrandId; CategoryId = input.CategoryId;  Description = input.Description}]
                                | true ->
                                    failwith (sprintf "A product with name %s already exists" input.Name)

                                return id
                              })
            ])

    let executor = Schema(query = Query, mutation = Mutation) :> ISchema<Root> |> Executor
    let root = { ClientId = System.Guid.NewGuid() }