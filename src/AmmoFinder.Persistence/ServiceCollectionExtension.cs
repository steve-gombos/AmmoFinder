using AmmoFinder.Common.Interfaces;
using AmmoFinder.Data;
using AmmoFinder.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmmoFinder.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProductPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRefreshProducts, RefreshProducts>();
            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddTransient<IProductsRepository, ProductsRepository>();

            services.AddDbContext<ProductsContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ProductsContext).Assembly.GetName().Name);
                });
            });

            return services;
        }
    }
}
