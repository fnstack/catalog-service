module Catalog.API.ProductSchema

open FSharp.Data.GraphQL.Types

// TYPES
type ProductType = {Id: System.Guid; Name: string; BrandId: string; CategoryId: string; Description: string option}

let rec ProductType=
          Define.Object<ProductType>(
            name = "Product",
            description = "A product .",
            isTypeOf = (fun o -> o :? ProductType),
            fieldsFn = fun () ->
            [
                Define.Field("id", ID<System.Guid>, "The id of the product.", fun _ dto -> dto.Id)
                Define.Field("name", String, "The name of the product.", fun _ dto -> dto.Name)
                Define.Field("brandId", ID<System.String>, "The Parentid of the product.", fun _ dto -> dto.BrandId)
                Define.Field("categoryId", ID<System.String>, "The name of the product.", fun _ dto -> dto.CategoryId)
                Define.Field("description", Nullable String, "The description of the product.", fun _ dto -> dto.Description)
            ])



