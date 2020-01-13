using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TestFramework.Test.Models;

namespace TestFramework.Test.Services
{
    public class ProductCreator
    {
        public static Product CreateProduct(IConfiguration configuration)
        {
            var product = new Product
            {
                MinPrice = configuration.GetValue<int>("product:minPrice"),
                MaxPrice = configuration.GetValue<int>("product:maxPrice"),
                Brand = configuration["product:brand"],
                Type = configuration["product:type"],
                ClearCartText = configuration["product:clearCartText"]
            };
            return product;
        }
    }
}
