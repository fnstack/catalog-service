module Catalog.API.ProductBrandSchema

open FSharp.Data.GraphQL.Types

type ProductBrandDto = {Id: System.Guid; Name: string; Description: string}

type CreateProductBrandInput = { Name: string }

let CreateProductBrandInput =
        Define.InputObject<CreateProductBrandInput>("CreateProductBrandInput",
            [ Define.Input("name", String) ])

let rec ProductBrandType =
    Define.Object<ProductBrandDto>(
            name = "ProductBrand",
            description = "A product brand.",
            isTypeOf = (fun o -> o :? ProductBrandDto),
            fieldsFn = fun () ->
            [
                Define.Field("id", ID<System.Guid>, "The id of the product brand.", fun _ dto -> dto.Id)
                Define.Field("name", String, "The name of the product brand.", fun _ dto -> dto.Name)
                Define.Field("description", String, "The description of the product brand.", fun _ dto -> dto.Description)
            ])
