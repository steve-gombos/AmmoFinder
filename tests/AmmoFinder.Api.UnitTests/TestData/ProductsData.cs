using AmmoFinder.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace AmmoFinder.Api.UnitTests.TestData
{
    public class ProductsData : TheoryData<IEnumerable<ProductModel>, Type>
    {
        public ProductsData()
        {
            Add(new List<ProductModel>
            {
                new ProductModel
                {
                    Brand = "test",
                    Caliber = "9mm",
                    Casing = "brass",
                    Description = "9mm ammo description",
                    Grain = "120",
                    Inventory = 10,
                    IsAvailable = true,
                    Name = "9mm Ammo",
                    Price = 10.99m,
                    RoundCount = "50",
                    RoundType = "FMJ",
                    UpdatedOn = DateTime.Now,
                    Url = "https://test.test/test-product"
                }
            }, typeof(OkObjectResult));

            Add(new List<ProductModel>(), typeof(NotFoundResult));
        }
    }
}
