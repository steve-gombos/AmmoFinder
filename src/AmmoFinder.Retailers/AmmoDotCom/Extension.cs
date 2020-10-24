using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AmmoFinder.Retailers.AmmoDotCom
{
    public static class Extension
    {
        public const string BaseUrl = "https://ammo.com/";

        public static IServiceCollection AddAmmoDotComClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.AmmoDotCom, client =>
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Add("Accept", "text/html");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true
                    };
                })
                .ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(20, TimeSpan.FromMinutes(1)));

            return services;
        }
    }
}
