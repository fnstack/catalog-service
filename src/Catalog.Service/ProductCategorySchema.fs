module Catalog.API.ProductCategorySchema

open FSharp.Data.GraphQL.Types

// INPUTS
type CreateProductCategoryInput = { Name: string; ParentId: string; Description: string }

let CreateProductCategoryInput =
        Define.InputObject<CreateProductCategoryInput>("CreateProductCategoryInput",
            [ Define.Input("name", String)
              Define.Input("parentId", ID<System.String>)
              Define.Input("description", String)
            ])

// TYPES
type ProductCategoryDto = {Id: System.Guid; Name: string; ParentId: string; Description: string}

let rec ProductCategoryType =
        Define.Object<ProductCategoryDto>(
            name = "ProductCategory",
            description = "A product Category.",
            isTypeOf = (fun o -> o :? ProductCategoryDto),
            fieldsFn = fun () ->
            [
                Define.Field("id", ID<System.Guid>, "The id of the product Category.", fun _ dto -> dto.Id)
                Define.Field("name", String, "The name of the product Category.", fun _ dto -> dto.Name)
                Define.Field("parentid", ID<System.String>, "The Parentid of the product Category.", fun _ dto -> dto.ParentId)
                Define.Field("description", String, "The description of the product Category.", fun _ dto -> dto.Description)
            ])

