using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AmmoFinder.Retailers.AimSurplus
{
    public static class Extension
    {
        public const string BaseUrl = "https://web.aimsurplus.com/";

        public static IServiceCollection AddAimSurplusClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.AimSurplus, client =>
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true
                    };
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(25, TimeSpan.FromMinutes(1)));

            return services;
        }
    }
}
