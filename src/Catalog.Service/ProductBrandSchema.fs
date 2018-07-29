namespace Catalog.API

open FSharp.Data.GraphQL.Types

type ProductBrandDto = {Id: System.Guid; Name: System.String}

module ProductBrandSchema =
    let rec ProductBrandType =
        Define.Object<ProductBrandDto>(
                name = "ProductBrand",
                description = "A product brand.",
                isTypeOf = (fun o -> o :? ProductBrandDto),
                fieldsFn = fun () ->
                [
                    Define.Field("id", ID<System.Guid>, "The id of the product brand.", fun _ dto -> dto.Id)
                    Define.Field("name", String, "The name of the product brand.", fun _ dto -> dto.Name)
                ])
