using Assets;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests
{
    public class VectorExtensionsTests
    {
        [Fact]
        public void ClampedMagnitudeDoesNothingIfMagnitudeIsWithinMax()
        {
            var sut = new Vector3(1, 0, 0);

            //sut.ClampLength(10).Should().Be(new Vector3(1, 0, 0));
        }

        [Fact]
        public void ClampedMagnitudeDoesNothingIfMagnitudeIsWithinMax2()
        {
            var sut = new Vector3(1, 0, 0);

            //sut.ClampLength(10).Should().Be(new Vector3(1, 0, 0));
        }
    }
}