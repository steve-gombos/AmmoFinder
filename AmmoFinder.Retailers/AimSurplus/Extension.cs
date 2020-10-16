using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace AmmoFinder.Retailers.AimSurplus
{
    public static class Extension
    {
        public const string BaseUrl = "https://web.aimsurplus.com/";

        public static IServiceCollection AddAimSurplusClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.AimSurplus, config =>
                {
                    config.BaseAddress = new Uri(BaseUrl);
                    config.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(25, TimeSpan.FromMinutes(1)));

            return services;
        }
    }
}
