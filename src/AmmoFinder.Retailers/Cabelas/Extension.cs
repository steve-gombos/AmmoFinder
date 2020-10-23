using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AmmoFinder.Retailers.Cabelas
{
    public static class Extension
    {
        public const string BaseUrl = "https://www.cabelas.com/shop/";

        public static IServiceCollection AddCabelasClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.Cabelas, client =>
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Add("Accept", "text/html");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    client.DefaultRequestHeaders.Add("Origin", "https://www.cabelas.com");
                    client.DefaultRequestHeaders.Add("Host", "www.cabelas.com");
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true
                    };
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
