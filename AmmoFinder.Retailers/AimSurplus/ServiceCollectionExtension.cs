using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace AmmoFinder.Retailers.AimSurplus
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAimSurplusClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.AimSurplus, config =>
                {
                    config.BaseAddress = new Uri("https://web.aimsurplus.com/");
                    config.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(25, TimeSpan.FromMinutes(1)));

            return services;
        }
    }
}
