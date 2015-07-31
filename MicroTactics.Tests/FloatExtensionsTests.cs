using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests
{
    public class FloatExtensionsTests
    {
        [Fact]
        public void ValueWithinToleranceShouldBeTrue()
        {
            0.0f.IsApproximately(0.000001f, 0.0001f).Should().BeTrue();
        }

        [Fact]
        public void ValueWithinToleranceShouldBeFalse()
        {
            0.0f.IsApproximately(0.001f, 0.0001f).Should().BeFalse();
        }

        [Fact]
        public void DefaultsToEpsilonForTolerance()
        {
            0.0f.IsApproximately(0.0f + float.Epsilon/2).Should().BeTrue();
        }
    }
}