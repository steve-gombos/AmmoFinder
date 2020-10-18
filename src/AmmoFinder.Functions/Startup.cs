﻿using AmmoFinder.Persistence;
using AmmoFinder.Persistence.Services;
using AmmoFinder.Retailers;
using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var context = builder.GetContext();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddMaps(new List<Assembly> { typeof(ProductServiceBase).Assembly, typeof(RefreshProducts).Assembly });
            });

            builder.Services.AddProductPersistence(context.Configuration.GetConnectionString("Products"));
            builder.Services.AddRetailers();
            builder.Services.AddLogging();
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
