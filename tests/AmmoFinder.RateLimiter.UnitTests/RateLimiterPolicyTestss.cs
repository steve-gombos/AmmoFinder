using FluentAssertions;
using Polly;
using System;
using Xunit;

namespace AmmoFinder.RateLimiter.UnitTests
{
    public class RateLimiterPolicyTestss
    {
        [Fact]
        public void ReplaceMeWithRealTests()
        {
            /*
             * This test is for illustrative purposes, to show the interfaces a typical synchronous non-generic policy fulfills.
             * Real tests should check policy behaviour.
             */
            RateLimiterPolicy policy = RateLimiterPolicy.Create(2, TimeSpan.FromSeconds(30));

            policy.Should().BeAssignableTo<ISyncPolicy>();
            policy.Should().BeAssignableTo<IRateLimiterPolicy>();
        }

        [Fact]
        public void PolicyExecutesThePassedDelegate()
        {
            bool executed = false;
            RateLimiterPolicy policy = RateLimiterPolicy.Create(2, TimeSpan.FromSeconds(30));

            policy.Execute(() => executed = true);

            executed.Should().BeTrue();
        }

    }
}
