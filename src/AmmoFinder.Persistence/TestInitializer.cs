using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace AmmoFinder.Data
{
    public class ProductsContextFactory : IDesignTimeDbContextFactory<ProductsContext>
    {
        ProductsContext IDesignTimeDbContextFactory<ProductsContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

            return new ProductsContext(optionsBuilder.Options);
        }
    }
}
