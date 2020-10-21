using FluentAssertions;
using Polly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.RateLimiter.UnitTests
{
    public class RateLimiterAsyncPolicyTResultTests
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical asynchronous generic policy fulfills.
             * Real tests should check policy behaviour.
             */
            RateLimiterAsyncPolicy<int> policy = RateLimiterAsyncPolicy<int>.Create(2, TimeSpan.FromSeconds(30));

            policy.Should().BeAssignableTo<IAsyncPolicy<int>>();
            policy.Should().BeAssignableTo<IRateLimiterPolicy<int>>();
        }

        [Fact]
        public async Task PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            RateLimiterAsyncPolicy<int> policy = RateLimiterAsyncPolicy<int>.Create(2, TimeSpan.FromSeconds(30));

            await policy.ExecuteAsync(() =>
            {
                executed = true;
                return Task.FromResult(0);
            });

            executed.Should().BeTrue();
        }

    }
}
