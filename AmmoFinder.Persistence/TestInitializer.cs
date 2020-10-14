using AmmoFinder.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

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
