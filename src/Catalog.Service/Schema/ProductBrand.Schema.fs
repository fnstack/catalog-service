module Catalog.API.ProductBrandSchema

open FSharp.Data.GraphQL.Types

// INPUTS
type CreateProductBrandInput = { Name: string }

let CreateProductBrandInput =
        Define.InputObject<CreateProductBrandInput>("CreateProductBrandInput",
            [ Define.Input("name", String) ])


// TYPES
type ProductBrandDto = {Id: System.Guid; Name: string; Description: string option}

let rec ProductBrandType =
    Define.Object<ProductBrandDto>(
            name = "ProductBrand",
            description = "A product brand.",
            isTypeOf = (fun o -> o :? ProductBrandDto),
            fieldsFn = fun () ->
            [
                Define.Field("id", ID<System.Guid>, "The id of the product brand.", fun _ dto -> dto.Id)
                Define.Field("name", String, "The name of the product brand.", fun _ dto -> dto.Name)
                Define.Field("description", Nullable String, "The description of the product brand.", fun _ dto -> dto.Description)
            ])

let mutable brands = [
                   {Id = "681f3f83-2580-4c54-ac0a-f18dd1b0d73a" |> System.Guid; Name = "Apple"; Description = "Un des leader GAFA" |> Some}
                   {Id = "cc6b592e-b344-4daa-85d9-85ff501dc59c" |> System.Guid; Name = "Nokia"; Description = None}
                   {Id = "c79fdfc5-cfa8-43ac-8617-9df4b94c4cd1" |> System.Guid; Name = "Samsung"; Description = None}
                 ]
