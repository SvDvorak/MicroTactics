using System;
using FluentAssertions;
using UnityEngine;
using Xunit;

namespace MicroTactics.Tests
{
    public class FormationTests
    {
        [Fact]
        public void Expands_formation_using_columns_to_row_relation()
        {
            var sut = new Formation(2/2);
            var unit1 = new Unit(new Vector3());
            var unit2 = new Unit(new Vector3());
            var unit3 = new Unit(new Vector3());
            var unit4 = new Unit(new Vector3());

            sut.Add(unit1);
            sut.Add(unit2);
            sut.Add(unit3);
            sut.Add(unit4);

            unit1.Position.Should().Be(new Vector3Adapter(0, 0, 0));
            unit2.Position.Should().Be(new Vector3Adapter(1, 0, 0));
            unit3.Position.Should().Be(new Vector3Adapter(0, 1, 0));
            unit4.Position.Should().Be(new Vector3Adapter(1, 1, 0));
        }
    }
}
