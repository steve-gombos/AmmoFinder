using FluentAssertions;
using Polly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.RateLimiter.UnitTests
{
    public class RateLimiterAsyncPolicyTests
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical asynchronous non-generic policy fulfills.
             * Real tests should check policy behaviour.
             */
            RateLimiterAsyncPolicy policy = RateLimiterAsyncPolicy.Create(2, TimeSpan.FromSeconds(30));

            policy.Should().BeAssignableTo<IAsyncPolicy>();
            policy.Should().BeAssignableTo<IRateLimiterPolicy>();
        }

        [Fact]
        public async Task PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            RateLimiterAsyncPolicy policy = RateLimiterAsyncPolicy.Create(2, TimeSpan.FromSeconds(30));

            await policy.ExecuteAsync(() =>
            {
                executed = true;
                return Task.CompletedTask;
            });

            executed.Should().BeTrue();
        }

    }
}
