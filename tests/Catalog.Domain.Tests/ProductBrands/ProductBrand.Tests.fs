module Catalog.Domain.Tests.ProductBrand

open Expecto
open System
open Catalog.Domain

[<Tests>]
let tests =
    testList "ProductBrand" [
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

            testList "GetValue" [
                test "Should right value be returned" {

                  // Arrange
                  let value = Guid.NewGuid()
                  let id = value |> ProductBrandId.create |> fun result -> match result with Ok value -> value | _ -> ProductBrandId.Empty

                  // Act
                  let result = id |> ProductBrandId.getValue

                  // Assert
                  Expect.equal value result "Not equals"
                }
            ]

            testList "Empty" [
                test "Should be equals" {

                  // Arrange
                  let expectedValue = Guid.Empty

                  // Act
                  let actualValue = ProductBrandId.Empty |> ProductBrandId.getValue

                  // Assert
                  Expect.equal expectedValue actualValue "Not equals"
                }
            ]
          ]

        testList "ProductBrandName" [
            testList "Create" [
                test "Should return error on empty value" {

                  // Arrange
                  let value = String.Empty

                  // Act
                  let result = value |> ProductBrandName.create

                  // Assert
                  Expect.isError result "Value is empty"
                }

                test "Should return ok on correct value" {

                  // Arrange
                  let value = "Apple"

                  // Act
                  let result = value |> ProductBrandName.create

                  // Assert
                  Expect.isOk result "Value is incorrect"
                }

            ]

            testList "GetValue" [
                test "Should right value be returned" {

                  // Arrange
                  let value = "Apple"
                  let name = value |> ProductBrandName.create |> fun result -> match result with Ok value -> value | _ -> ProductBrandName.Empty

                  // Act
                  let result = name |> ProductBrandName.getValue

                  // Assert
                  Expect.equal value result "Not equals"
                }

                testList "Empty" [
                  test "Should be equals" {

                  // Arrange
                  let expectedValue = String.Empty

                  // Act
                  let actualValue = ProductBrandName.Empty |> ProductBrandName.getValue

                  // Assert
                  Expect.equal expectedValue actualValue "Not equals"
                }
            ]
              ]
          ]
  ]

