namespace Catalog.API

open FSharp.Data.GraphQL.Types

type ProductCategoryDto = {Id: System.Guid; Name: System.String; ParentId: System.Guid}

module ProductCategorySchema =
    let rec ProductCategoryType =
         Define.Object<ProductCategoryDto>(
               name = "ProductCategory",
               description = "A product Category.",
               isTypeOf = (fun o -> o :? ProductCategoryDto),
               fieldsFn = fun () ->
               [
                   Define.Field("id", ID<System.Guid>, "The id of the product Category.", fun _ dto -> dto.Id)
                   Define.Field("name", String, "The name of the product Category.", fun _ dto -> dto.Name)
                   Define.Field("parentid", ID<System.Guid>, "The Parentid of the product Category.", fun _ dto -> dto.ParentId)
               ])

