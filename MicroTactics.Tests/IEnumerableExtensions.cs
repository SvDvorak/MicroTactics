using System.Collections.Generic;
using FluentAssertions;
using Mono.GameMath;

namespace MicroTactics.Tests
{
    public static class IEnumerableExtensions
    {
        private const float Tolerance = 0.00001f;

        public static void ShouldAllBeEquivalentTo(this IEnumerable<Vector2> actual, IEnumerable<Vector2> expected)
        {
            actual.Should().Equal(expected, (v1, v2) =>
                v1.X.IsApproximately(v2.X, Tolerance) &&
                v1.Y.IsApproximately(v2.Y, Tolerance));
        }
    }
}