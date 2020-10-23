using AmmoFinder.Common.Interfaces;
using AmmoFinder.Data;
using AmmoFinder.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AmmoFinder.Persistence
{
    public static class ServiceCollectionExtension
    {
        public static string MigrationAssembly = typeof(ProductsContext).Assembly.GetName().Name;

        public static IServiceCollection AddProductPersistence(this IServiceCollection services, Action<DbContextOptionsBuilder> options = null)
        {
            services.AddTransient<IRefreshProducts, RefreshProducts>();
            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddTransient<IProductsRepository, ProductsRepository>();

            services.AddDbContext<ProductsContext>(options);

            services.Configure<SqlServerDbContextOptionsBuilder>(options =>
            {
                options.MigrationsAssembly(typeof(ProductsContext).Assembly.GetName().Name);
            });

            return services;
        }
    }
}
