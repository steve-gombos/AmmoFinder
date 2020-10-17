using Polly;

namespace AmmoFinder.RateLimiter
{
    /// <summary>
    /// Defines properties common to synchronous and asynchronous ProactiveFoo policies.
    /// </summary>
    public interface IRateLimiterPolicy : IsPolicy
    {
        /* Define properties (if any) or methods (if any) you may want to expose on RateLimiterPolicy.

         - Perhaps the custom policy takes configuration properties which you want to expose.
         - Perhaps the custom policy exposes methods for manual control.

        ... but it is equally common to have none.
         */
    }

    /// <summary>
    /// Defines properties common to generic, synchronous and asynchronous ProactiveFoo policies.
    /// </summary>
    public interface IRateLimiterPolicy<TResult> : IRateLimiterPolicy
    {
        /* Define properties (if any) or methods (if any) you may want to expose on RateLimiterPolicy<TResult>.

           Typically, IRateLimiterPolicy<TResult> : IRateLimiterPolicy, so you would only expose here any 
           extra properties/methods typed in <TResult> for TResult policies.
         */
    }
}