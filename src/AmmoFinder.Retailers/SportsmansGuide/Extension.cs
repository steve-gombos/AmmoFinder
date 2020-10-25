using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using CloudflareSolverRe;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AmmoFinder.Retailers.SportsmansGuide
{
    public static class Extension
    {
        public const string BaseUrl = "https://www.sportsmansguide.com/";

        public static IServiceCollection AddSportsmansGuideClient(this IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(RetailerNames.SportsmansGuide, client =>
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Add("Accept", "text/html");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true
                    };
                })
                .ConfigurePrimaryHttpMessageHandler(config => new ClearanceHandler
                {
                    InnerHandler = new HttpClientHandler
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
                    },
                    MaxTries = 3,
                    ClearanceDelay = 3000
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(25, TimeSpan.FromMinutes(1)));

            return services;
        }
    }
}
