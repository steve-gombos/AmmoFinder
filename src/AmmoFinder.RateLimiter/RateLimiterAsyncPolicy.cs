using Polly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AmmoFinder.RateLimiter
{
    /// <summary>
    /// A ProactiveFoo policy that can be applied to asynchronous delegates.
    /// </summary>
    public class RateLimiterAsyncPolicy : AsyncPolicy, IRateLimiterPolicy
    {
        private readonly int _limitCount;
        private readonly TimeSpan _limitTime;
        private readonly List<DateTimeOffset> _callLog = new List<DateTimeOffset>();

        /* This is the non-generic ProactiveFooPolicy for asynchronous executions.
         * With this policy, users can asynchronously execute Task-returning methods, using .ExecuteAsync(...),
         * or TResult-returning Func<Task<TResult>>s, using the generic base-class _method_ .ExecuteAsync<TResult>(...).
         * So, although the policy is non-generic, the Implementation<AsyncTResult>(...) method is generic in TResult. 
         */

        /* It is a syntax convention for proactive Polly policies to use static creation methods rather than use constructors directly. It makes the syntax more similar to reactive policy syntax. */

        /// <summary>
        /// Constructs a new instance of <see cref="RateLimiterAsyncPolicy"/>.
        /// </summary>
        /// <returns><see cref="RateLimiterAsyncPolicy"/></returns>
        public static RateLimiterAsyncPolicy Create(int limitCount, TimeSpan limitTime)
        {
            return new RateLimiterAsyncPolicy(limitCount, limitTime);
        }

        internal RateLimiterAsyncPolicy(int limitCount, TimeSpan limitTime)
        {
            _limitCount = limitCount;
            _limitTime = limitTime;
            /* ... and the policy constructor can store configuration, for the implementation to use. */
        }

        /// <inheritdoc/>
        protected override Task<TResult> ImplementationAsync<TResult>(Func<Context, CancellationToken, Task<TResult>> action, Context context, CancellationToken cancellationToken,
            bool continueOnCapturedContext)
        {
            /* This method is intentionally a pass-through.
             Delegating to AsyncProactiveFooEngine.ImplementationAsync<TResult>(...) allows the code to use that single asynchronous implementation
             for both AsyncProactiveFooPolicy and AsyncProactiveFooPolicy<TResult>
             */
            return RateLimiterAsyncEngine.ImplementationAsync(
                action,
                context,
                cancellationToken,
                continueOnCapturedContext,
                _callLog,
                _limitCount,
                _limitTime);
        }
    }
}