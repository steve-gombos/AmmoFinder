using AmmoFinder.Common.Interfaces;
using AmmoFinder.RateLimiter;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.BulkAmmo
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBulkAmmoClient(this IServiceCollection services)
        {
            var maxParallelism = 10;
            var throttler = Policy.BulkheadAsync<HttpResponseMessage>(maxParallelism);

            services.AddHttpClient<IProductService, ProductService>(RetailerNames.BulkAmmo, client =>
                {
                    client.BaseAddress = new System.Uri("https://www.bulkammo.com/");
                    client.DefaultRequestHeaders.Add("Accept", "text/html");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                })
                .ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
                })
                .AddPolicyHandler(RateLimiterAsyncPolicy<HttpResponseMessage>.Create(25, TimeSpan.FromMinutes(1)));
                //.AddPolicyHandler(throttler);
                //.AddHttpMessageHandler(() => new RateLimitHttpMessageHandler(50, TimeSpan.FromMinutes(1)))
                //.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            return services;
        }
    }


    //public class RateLimitPolicy : AsyncPolicy<HttpResponseMessage>
    //{
    //    private readonly List<DateTimeOffset> _callLog =
    //        new List<DateTimeOffset>();
    //    private readonly TimeSpan _limitTime;
    //    private readonly int _limitCount;

    //    public RateLimitPolicy(int limitCount, TimeSpan limitTime)
    //    {
    //        _limitCount = limitCount;
    //        _limitTime = limitTime;
    //    }

    //    protected async override Task<HttpResponseMessage> ImplementationAsync(Func<Context, CancellationToken, Task<HttpResponseMessage>> action, Context context, CancellationToken cancellationToken, bool continueOnCapturedContext)
    //    {
    //        var now = DateTimeOffset.UtcNow;

    //        lock (_callLog)
    //        {
    //            _callLog.Add(now);

    //            while (_callLog.Count > _limitCount)
    //                _callLog.RemoveAt(0);
    //        }

    //        await LimitDelay(now);

    //        return await action(context, cancellationToken);
    //    }

    //    private async Task LimitDelay(DateTimeOffset now)
    //    {
    //        if (_callLog.Count < _limitCount)
    //            return;

    //        var limit = now.Add(-_limitTime);

    //        var lastCall = DateTimeOffset.MinValue;
    //        var shouldLock = false;

    //        lock (_callLog)
    //        {
    //            lastCall = _callLog.FirstOrDefault();
    //            shouldLock = _callLog.Count(x => x >= limit) >= _limitCount;
    //        }

    //        var delayTime = shouldLock && (lastCall > DateTimeOffset.MinValue)
    //            ? (limit - lastCall)
    //            : TimeSpan.Zero;

    //        if (delayTime > TimeSpan.Zero)
    //            await Task.Delay(delayTime);
    //    }
    //}

    //public class RateLimitPolicy<TResult> : AsyncPolicy<TResult>
    //{
    //    private readonly List<DateTimeOffset> _callLog =
    //        new List<DateTimeOffset>();
    //    private readonly TimeSpan _limitTime;
    //    private readonly int _limitCount;

    //    private RateLimitPolicy(int limitCount, TimeSpan limitTime)
    //    {
    //        _limitCount = limitCount;
    //        _limitTime = limitTime;
    //    }

    //    public static RateLimitPolicy<TResult> Create(int limitCount, TimeSpan limitTime) => new RateLimitPolicy<TResult>(limitCount, limitTime);

    //    protected async override Task<TResult> ImplementationAsync(Func<Context, CancellationToken, Task<TResult>> action, Context context, CancellationToken cancellationToken, bool continueOnCapturedContext)
    //    {
    //        var now = DateTimeOffset.UtcNow;

    //        lock (_callLog)
    //        {
    //            _callLog.Add(now);

    //            while (_callLog.Count > _limitCount)
    //                _callLog.RemoveAt(0);
    //        }

    //        await LimitDelay(now);

    //        return await action(context, cancellationToken);
    //    }

    //    private async Task LimitDelay(DateTimeOffset now)
    //    {
    //        if (_callLog.Count < _limitCount)
    //            return;

    //        var limit = now.Add(-_limitTime);

    //        var lastCall = DateTimeOffset.MinValue;
    //        var shouldLock = false;

    //        lock (_callLog)
    //        {
    //            lastCall = _callLog.FirstOrDefault();
    //            shouldLock = _callLog.Count(x => x >= limit) >= _limitCount;
    //        }

    //        var delayTime = shouldLock && (lastCall > DateTimeOffset.MinValue)
    //            ? (limit - lastCall)
    //            : TimeSpan.Zero;

    //        if (delayTime > TimeSpan.Zero)
    //            await Task.Delay(delayTime);
    //    }
    //}

    public class RateLimitHttpMessageHandler : DelegatingHandler
    {
        private readonly List<DateTimeOffset> _callLog =
            new List<DateTimeOffset>();
        private readonly TimeSpan _limitTime;
        private readonly int _limitCount;

        public RateLimitHttpMessageHandler(int limitCount, TimeSpan limitTime)
        {
            _limitCount = limitCount;
            _limitTime = limitTime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow;

            lock (_callLog)
            {
                _callLog.Add(now);

                while (_callLog.Count > _limitCount)
                    _callLog.RemoveAt(0);
            }

            await LimitDelay(now);

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task LimitDelay(DateTimeOffset now)
        {
            if (_callLog.Count < _limitCount)
                return;

            var limit = now.Add(-_limitTime);

            var lastCall = DateTimeOffset.MinValue;
            var shouldLock = false;

            lock (_callLog)
            {
                lastCall = _callLog.FirstOrDefault();
                shouldLock = _callLog.Count(x => x >= limit) >= _limitCount;
            }

            var delayTime = shouldLock && (lastCall > DateTimeOffset.MinValue)
                ? (limit - lastCall)
                : TimeSpan.Zero;

            if (delayTime > TimeSpan.Zero)
                await Task.Delay(delayTime);
        }
    }
}
