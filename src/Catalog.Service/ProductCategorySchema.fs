module Catalog.API.ProductCategorySchema

open FSharp.Data.GraphQL.Types

// INPUTS
type CreateProductCategoryInput = { Name: string; ParentId: string option; Description: string option}

let CreateProductCategoryInput =
        Define.InputObject<CreateProductCategoryInput>("CreateProductCategoryInput",
            [ Define.Input("name", String)
              Define.Input("parentId", Nullable ID<System.String>)
              Define.Input("description", Nullable String)
            ])

// TYPES
type ProductCategoryDto = {Id: System.Guid; Name: string; ParentId: string option; Description: string option}

let rec ProductCategoryType =
        Define.Object<ProductCategoryDto>(
            name = "ProductCategory",
            description = "A product Category.",
            isTypeOf = (fun o -> o :? ProductCategoryDto),
            fieldsFn = fun () ->
            [
                Define.Field("id", ID<System.Guid>, "The id of the product Category.", fun _ dto -> dto.Id)
                Define.Field("name", String, "The name of the product Category.", fun _ dto -> dto.Name)
                Define.Field("parentid", Nullable String, "The Parentid of the product Category.", fun _ dto -> dto.ParentId)
                Define.Field("description", Nullable String, "The description of the product Category.", fun _ dto -> dto.Description)
            ])

