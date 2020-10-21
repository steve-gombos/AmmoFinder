using Polly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AmmoFinder.RateLimiter
{
    /// <summary>
    /// A ProactiveFoo policy that can be applied to asynchronous delegates returning a value of type <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of return values this policy will handle.</typeparam>
    public class RateLimiterAsyncPolicy<TResult> : AsyncPolicy<TResult>, IRateLimiterPolicy<TResult>
    {
        private readonly int _limitCount;
        private readonly TimeSpan _limitTime;
        private readonly List<DateTimeOffset> _callLog = new List<DateTimeOffset>();

        /* This is the generic ProactiveFooPolicy for asynchronous executions.
         * With this policy, users can execute TResult-returning Func<Task<TResult>>s.
         */

        /* It is a syntax convention for proactive Polly policies to use static creation methods rather than use constructors directly. It makes the syntax more similar to reactive policy syntax. */

        /// <summary>
        /// Constructs a new instance of <see cref="RateLimiterAsyncPolicy{TResult}"/>.
        /// </summary>
        /// <returns><see cref="RateLimiterAsyncPolicy{TResult}"/></returns>
        public static RateLimiterAsyncPolicy<TResult> Create(int limitCount, TimeSpan limitTime)
        {
            return new RateLimiterAsyncPolicy<TResult>(limitCount, limitTime);
        }

        internal RateLimiterAsyncPolicy(int limitCount, TimeSpan limitTime)
        {
            _limitCount = limitCount;
            _limitTime = limitTime;
        }

        /// <inheritdoc/>
        protected override Task<TResult> ImplementationAsync(Func<Context, CancellationToken, Task<TResult>> action, Context context, CancellationToken cancellationToken,
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