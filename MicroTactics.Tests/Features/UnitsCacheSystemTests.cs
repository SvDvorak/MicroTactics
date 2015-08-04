using System.Collections.Generic;
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
            _sut.trigger.Should().Be(Matcher.Unit);
            _sut.eventType.Should().Be(GroupEventType.OnEntityAddedOrRemoved);
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
            _pool.CreateEntity().AddUnit(0);
            var unit = _pool.CreateEntity().AddUnit(0);

            _sut.Execute(unit.AsList());

            var cachedUnits = _squad.unitsCache.Units;
            cachedUnits.Should().HaveCount(1);
            cachedUnits[0].Should().Be(unit);
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