using Polly;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AmmoFinder.RateLimiter
{
    internal static class RateLimiterEngine
    {
        internal static TResult Implementation<TResult>(
            Func<Context, CancellationToken, TResult> action, // The delegate the user passed to execute 
            Context context, // The context the user passed to execute (never null; Polly provides one if user does not)
            CancellationToken cancellationToken, // The cancellation token the user passed to execute; for co-operative cancellation to be effective, policy implementations should honour it at suitable points in the execution.
            List<DateTimeOffset> callLog,
            int limitCount,
            TimeSpan limitTime)
        {
            Delayer.LimitDelay(callLog, limitCount, limitTime).RunSynchronously();

            TResult result = action(context, cancellationToken);

            return result;
        }
    }
}
