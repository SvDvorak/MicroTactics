using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets;
using FluentAssertions;
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
    }
}
