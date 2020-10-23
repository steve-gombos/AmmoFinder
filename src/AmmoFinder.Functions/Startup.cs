using AmmoFinder.Persistence;
using AmmoFinder.Persistence.Services;
using AmmoFinder.Retailers;
using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

[assembly: FunctionsStartup(typeof(AmmoFinder.Functions.Startup))]

namespace AmmoFinder.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddAutoMapper(config =>
            {
                config.AddMaps(new List<Assembly> { typeof(ProductServiceBase).Assembly, typeof(RefreshProducts).Assembly });
            });

            builder.Services.AddProductPersistence(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Products"));
            });
            builder.Services.AddRetailers();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
    }
}
