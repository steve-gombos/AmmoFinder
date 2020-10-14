﻿using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;

namespace AmmoFinder.Retailers.Cabelas
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCabelasClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.Cabelas, client =>
                {
                    client.BaseAddress = new System.Uri("https://www.cabelas.com/shop/");
                    client.DefaultRequestHeaders.Add("Accept", "text/html");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    client.DefaultRequestHeaders.Add("Origin", "https://www.cabelas.com");
                })
                .ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(25, TimeSpan.FromMinutes(1)));

            return services;
        }
    }
}