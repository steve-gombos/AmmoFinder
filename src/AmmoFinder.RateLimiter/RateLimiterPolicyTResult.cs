using Polly;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AmmoFinder.RateLimiter
{
    /// <summary>
    /// A ProactiveFoo policy that can be applied to delegates returning a value of type <typeparamref name="TResult" />
    /// </summary>
    /// <typeparam name="TResult">The type of return values this policy will handle.</typeparam>
    public class RateLimiterPolicy<TResult> : Policy<TResult>, IRateLimiterPolicy<TResult>
    {
        private readonly int _limitCount;
        private readonly TimeSpan _limitTime;
        private readonly List<DateTimeOffset> _callLog = new List<DateTimeOffset>();

        /* This is the generic ProactiveFooPolicy for synchronous executions.
         * With this policy, users can execute TResult-returning Func<>s.
         */

        /* It is a syntax convention for proactive Polly policies to use static creation methods rather than use constructors directly. It makes the syntax more similar to reactive policy syntax. */

        /// <summary>
        /// Constructs a new instance of <see cref="RateLimiterPolicy{TResult}"/>.
        /// </summary>
        /// <returns><see cref="RateLimiterPolicy{TResult}"/></returns>
        public static RateLimiterPolicy<TResult> Create(int limitCount, TimeSpan limitTime)
        {
            return new RateLimiterPolicy<TResult>(limitCount, limitTime);
        }

        internal RateLimiterPolicy(int limitCount, TimeSpan limitTime)
        {
            _limitCount = limitCount;
            _limitTime = limitTime;
        }

        /// <inheritdoc/>
        protected override TResult Implementation(Func<Context, CancellationToken, TResult> action, Context context, CancellationToken cancellationToken)
        {
            /* This method is intentionally a pass-through.
             Delegating to ProactiveFooEngine.Implementation<TResult>(...) allows the code to use that single synchronous implementation
             for both ProactiveFooPolicy.Execute<TResult>() and ProactiveFooPolicy<TResult>.Execute(...)
             */
            return RateLimiterEngine.Implementation(
                action,
                context,
                cancellationToken,
                _callLog,
                _limitCount,
                _limitTime);
        }
    }
}