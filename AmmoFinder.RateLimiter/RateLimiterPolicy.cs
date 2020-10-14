using Polly;
using System;
using System.Threading;

namespace AmmoFinder.RateLimiter
{
    /// <summary>
    /// A ProactiveFoo policy that can be applied to delegates.
    /// </summary>
    public class RateLimiterPolicy : Policy, IRateLimiterPolicy
    {
        private readonly int _limitCount;
        private readonly TimeSpan _limitTime;

        /* This is the non-generic ProactiveFooPolicy for synchronous executions.
         * With this policy, users can execute void-returning Actions, using .Execute(...),
         * or TResult-returning Func<>s, using the generic base-class _method_ .Execute<TResult>(...).
         * So, although the policy is non-generic, the Implementation<TResult>(...) method is generic in TResult. 
         */

        /* It is a syntax convention for proactive Polly policies to use static creation methods rather than use constructors directly. It makes the syntax more similar to reactive policy syntax. */

        /// <summary>
        /// Constructs a new instance of <see cref="RateLimiterPolicy"/>.
        /// </summary>
        /// <returns><see cref="RateLimiterPolicy"/></returns>
        public static RateLimiterPolicy Create(int limitCount, TimeSpan limitTime)
        {
            return new RateLimiterPolicy(limitCount, limitTime);
        }

        internal RateLimiterPolicy(int limitCount, TimeSpan limitTime)
        {
            _limitCount = limitCount;
            _limitTime = limitTime;
        }

        /// <inheritdoc/>
        protected override TResult Implementation<TResult>(Func<Context, CancellationToken, TResult> action, Context context, CancellationToken cancellationToken)
        {
            /* This method is intentionally a pass-through.
             Delegating to ProactiveFooEngine.Implementation<TResult>(...) allows the code to use that single synchronous implementation
             for both ProactiveFooPolicy and ProactiveFooPolicy<TResult>
             */
            return RateLimiterEngine.Implementation(
                action,
                context,
                cancellationToken,
                _limitCount,
                _limitTime);
        }
    }
}
