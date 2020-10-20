using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmmoFinder.RateLimiter
{
    internal static class RateLimiterAsyncEngine
    {
        internal static async Task<TResult> ImplementationAsync<TResult>(
            Func<Context, CancellationToken, Task<TResult>> action, // The delegate the user passed to execute 
            Context context, // The context the user passed to execute (never null; Polly provides one if user does not) 
            CancellationToken cancellationToken, // The cancellation token the user passed to execute; for co-operative cancellation to be effective, policy implementations should honour it at suitable points in the execution.
            bool continueOnCapturedContext, // Whether to continue executions on captured context (ConfigureAwait(...)); defaults to false, if not user-specfied.
            List<DateTimeOffset> callLog,
            int limitCount,
            TimeSpan limitTime
            )
        {
            var now = DateTimeOffset.UtcNow;

            lock (callLog)
            {
                callLog.Add(now);

                while (callLog.Count > limitCount)
                    callLog.RemoveAt(0);
            }

            await LimitDelay(now, callLog, limitCount, limitTime);

            TResult result = await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext);

            return result;
        }

        private static async Task LimitDelay(DateTimeOffset now, List<DateTimeOffset> callLog, int limitCount, TimeSpan limitTime)
        {
            if (callLog.Count < limitCount)
                return;

            var limit = now.Add(-limitTime);

            var lastCall = DateTimeOffset.MinValue;
            var shouldLock = false;

            lock (callLog)
            {
                lastCall = callLog.FirstOrDefault();
                shouldLock = callLog.Count(x => x >= limit) >= limitCount;
            }

            var delayTime = shouldLock && (lastCall > DateTimeOffset.MinValue)
                ? (lastCall - limit)
                : TimeSpan.Zero;

            if (delayTime > TimeSpan.Zero)
                await Task.Delay(delayTime);
        }
    }
}
