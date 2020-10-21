using Polly;
using System;
using System.Collections.Generic;
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
            await Delayer.LimitDelay(callLog, limitCount, limitTime);

            TResult result = await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext);

            return result;
        }
    }
}
