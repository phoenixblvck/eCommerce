using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any()) {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }

            if(!context.ProductTypes.Any()) {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if(!context.Products.Any()) {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }
            
            if(!context.DeliveryMethods.Any()) {
                var deliveryMethodData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodData);
                context.DeliveryMethods.AddRange(deliveryMethod);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}