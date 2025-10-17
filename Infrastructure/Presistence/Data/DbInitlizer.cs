using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.ProdutModule;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data
{
    public class DbInitlizer : IDbInitlizer
    {
        private readonly StoreDbContext _storeDbContext;

        public DbInitlizer(StoreDbContext storeDbContext) {
            _storeDbContext = storeDbContext;
        }

        public async  Task InitializeAsync()
        {
            try
            {
                // create the database if it is not created & apply migrations 
                if (_storeDbContext.Database.GetPendingMigrations().Any())
                    await _storeDbContext.Database.MigrateAsync();

                // apply data seeding 
                if (!_storeDbContext.ProductTypes.Any())
                {
                    // Read Types From File As String 
                    var typeData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\DataSeeding\types.json");
                    // Transform From Json into C# Object
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                    // Adding Data to Database & Saving Changes
                    if (types != null && types.Count > 0)
                    {
                        await _storeDbContext.ProductTypes.AddRangeAsync(types);

                    }
                }

                if (!_storeDbContext.ProductBrands.Any())
                {
                    // Read Types From File As String 
                    var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\DataSeeding\brands.json");
                    // Transform From Json into C# Object
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    // Adding Data to Database & Saving Changes
                    if (brands != null && brands.Count > 0)
                    {
                        await _storeDbContext.ProductBrands.AddRangeAsync(brands);

                    }
                }

                if (!_storeDbContext.Products.Any())
                {
                    // Read Types From File As String 
                    var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\DataSeeding\products.json");
                    // Transform From Json into C# Object
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    // Adding Data to Database & Saving Changes
                    if (products != null && products.Count > 0)
                    {
                        await _storeDbContext.Products.AddRangeAsync(products);

                    }
                }
             

                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
