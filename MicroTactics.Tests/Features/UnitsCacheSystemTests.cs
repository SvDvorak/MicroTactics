using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class UnitsCacheSystemTests
    {
        private readonly UnitsCacheSystem _sut;
        private readonly Entity _squad;
        private readonly TestPool _pool;

        public UnitsCacheSystemTests()
        {
            _sut = new UnitsCacheSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);

            _squad = _pool.CreateEntity().AddSquad(0);
        }

        [Fact]
        public void TriggersOnAddedOrRemovedUnits()
        {
            _sut.triggers[0].Should().Be(Matcher.Unit);
            _sut.triggers[1].Should().Be(Matcher.AllOf(Matcher.Unit, Matcher.Destroy));
            _sut.eventTypes[0].Should().Be(GroupEventType.OnEntityAddedOrRemoved);
            _sut.eventTypes[1].Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void AddsCacheToSquadIfOneDoesNotExist()
        {
            _sut.Execute(new List<Entity>());

            _squad.hasUnitsCache.Should().BeTrue("squad should always have a cache");
        }

        [Fact]
        public void AddsNewUnitsToMatchingSquad()
        {
            var unit = _pool.CreateEntity().AddUnit(0);

            _sut.Execute(unit.AsList());

            var cachedUnits = _squad.unitsCache.Units;
            cachedUnits.Should().HaveCount(1);
            cachedUnits[0].Should().Be(unit);
        }

        [Fact]
        public void HandlesMultipleSquads()
        {
            var squad2 = _pool.CreateEntity().AddSquad(1);
            var unit1 = _pool.CreateEntity().AddUnit(0);
            var unit2 = _pool.CreateEntity().AddUnit(1);

            _sut.Execute(new [] { unit1, unit2 }.ToList());

            _squad.unitsCache.Units.Should().HaveCount(1);
            _squad.unitsCache.Units[0].Should().Be(unit1);

            squad2.unitsCache.Units.Should().HaveCount(1);
            squad2.unitsCache.Units[0].Should().Be(unit2);
        }

        [Fact]
        public void RemovesUnitsFromMatchingSquad()
        {
            var unit = _pool.CreateEntity().AddUnit(0);
            _sut.Execute(unit.AsList());

            var entityWithRemovedUnit = unit.RemoveUnit();
            _sut.Execute(entityWithRemovedUnit.AsList());

            var cachedUnits = _squad.unitsCache.Units;
            cachedUnits.Should().HaveCount(0);
        }

        [Fact]
        public void RemovesDestroyedUnits()
        {
            var unit = _pool.CreateEntity().AddUnit(0);
            _sut.Execute(unit.AsList());

            var destroyedUnit = unit.IsDestroy(true);
            _sut.Execute(destroyedUnit.AsList());

            var cachedUnits = _squad.unitsCache.Units;
            cachedUnits.Should().HaveCount(0);
        }
    }
}