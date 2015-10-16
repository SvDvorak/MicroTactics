using System;
using FluentAssertions;
using Mono.GameMath;

namespace MicroTactics.Tests.Features
{
    public static class CloseToExtensions
    {
        private static float _minimumDifference = 0.001f;

        public static void ShouldBeCloseTo(this Vector3 actual, Vector3 expected)
        {
            BeInRange(expected.X, actual.X);
            BeInRange(expected.Y, actual.Y);
            BeInRange(expected.Z, actual.Z);
        }

        public static void ShouldBeCloseTo(this Quaternion actual, Quaternion expected)
        {
            BeInRange(expected.X, actual.X);
            BeInRange(expected.Y, actual.Y);
            BeInRange(expected.Z, actual.Z);
            BeInRange(expected.W, actual.W);
        }

        private static void BeInRange(float expected, float actual)
        {
            actual.Should().BeInRange(expected - _minimumDifference, expected + _minimumDifference);
        }
    }
}