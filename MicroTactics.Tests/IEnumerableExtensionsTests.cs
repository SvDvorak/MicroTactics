using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets;
using FluentAssertions;
using UnityEngine;
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
        public void SumsVectors()
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
    }
}
