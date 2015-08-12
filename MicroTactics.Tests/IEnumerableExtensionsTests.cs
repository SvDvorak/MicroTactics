using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void GettingNumberedItemInEnumerableReturnsThatItem()
        {
            var ints = new List<int>() { 1, 2, 3, 4 };

            ints.First().Should().Be(1);
            ints.Second().Should().Be(2);
            ints.Third().Should().Be(3);
            ints.Fourth().Should().Be(4);
        }

        [Fact]
        public void SumsVector2s()
        {
            var vectors = new List<Vector2>()
                {
                    new Vector2(0, 0),
                    new Vector2(1, 2),
                    new Vector2(2, 3)
                };

            vectors.Sum().Should().Be(new Vector2(3, 5));
            vectors.Sum(x => x).Should().Be(new Vector2(3, 5));
        }

        [Fact]
        public void SumsVector3s()
        {
            var vectors = new List<Vector3>()
                {
                    new Vector3(0, 0, 0),
                    new Vector3(1, 2, 3),
                    new Vector3(2, 3, 4)
                };

            vectors.Sum().Should().Be(new Vector3(3, 5, 7));
            vectors.Sum(x => x).Should().Be(new Vector3(3, 5, 7));
        }

        [Fact]
        public void ReturnsEmptyVector3WhenNoElementsAreInList()
        {
            var emptyList = new List<Vector3>();
            emptyList.Sum(x => x).Should().Be(new Vector3(0, 0, 0));
        }

        [Fact]
        public void ReturnsEmptyVector2WhenNoElementsAreInList()
        {
            var emptyList = new List<Vector2>();
            emptyList.Sum(x => x).Should().Be(new Vector2(0, 0));
        }
    }
}
