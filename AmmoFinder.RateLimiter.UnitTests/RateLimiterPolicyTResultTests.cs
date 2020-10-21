using FluentAssertions;
using Polly;
using System;
using Xunit;

namespace AmmoFinder.RateLimiter.UnitTests
{
    public class RateLimiterPolicyTResultTests
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical synchronous generic policy fulfills.
             * Real tests should check policy behaviour.
             */

            RateLimiterPolicy<int> policy = RateLimiterPolicy<int>.Create(2, TimeSpan.FromSeconds(30));

            policy.Should().BeAssignableTo<ISyncPolicy<int>>();
            policy.Should().BeAssignableTo<IRateLimiterPolicy<int>>();
        }

        [Fact]
        public void PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            RateLimiterPolicy<int> policy = RateLimiterPolicy<int>.Create(2, TimeSpan.FromSeconds(30));

            policy.Execute(() =>
            {
                executed = true;
                return default(int);
            });

            executed.Should().BeTrue();
        }
    }
}
