using AmmoFinder.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmmoFinder.Common.Extensions
{
    //https://stackoverflow.com/questions/37780136/asp-core-migrate-ef-core-sql-db-on-startup
    public static class ApplicationBuilderExtensions
    {
        public static void UpdateDatabase<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<T>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public static void UpdateAndSeedDatabase<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<T>())
                {
                    context.Database.Migrate();

                    var seeder = serviceScope.ServiceProvider.GetService<IDataSeeder>();

                    seeder.Seed().Wait();
                }
            }
        }
    }
}
