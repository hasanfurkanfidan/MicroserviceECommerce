using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product>productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                GetConfiguredProducts(productCollection);
               
            }
        }

        private static void GetConfiguredProducts(IMongoCollection<Product> productCollection)
        {
            productCollection.InsertManyAsync(new List<Product>()
                {
                    new Product(){Name="Asus Laptop",
                    Price=500,
                    Category="Computer",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "ImageFile",
                    },
                    new Product()
                    {
                        Name = "Msi Laptop",
                        Category = "Computer",
                        Summary = "Summary",
                        Description = "Description",
                        ImageFile = "ImageFile"

                    },
                    new Product()
                    {
                        Name = "IPhoneX",
                        Category = "Phone",
                        Summary  ="Summary",
                        Description = "Description",
                        ImageFile = "ImageFile"
                    }

                });
        }
    }
}
