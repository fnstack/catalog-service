module Catalog.Domain.Tests.ProductBrand

open Expecto
open System
open Catalog.Domain

[<Tests>]
let tests =
  testList "ProductBrandId" [
    testList "Create" [
        test "Should return error on empty value" {

          // Arrange
          let value = Guid.Empty

          // Act
          let result = value |> ProductBrandId.create

          // Assert
          Expect.isError result "Value is empty"
        }

        test "Should return ok on correct value" {

          // Arrange
          let value = Guid.NewGuid()

          // Act
          let result = value |> ProductBrandId.create

          // Assert
          Expect.isOk result "Value is incorrect"
        }
    ]
  ]

