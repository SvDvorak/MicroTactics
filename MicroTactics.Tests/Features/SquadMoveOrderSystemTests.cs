using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SquadMoveOrderSystemTests
    {
        private readonly SquadMoveOrderSystem _sut;
        private readonly TestPool _testPool;
        private readonly Entity _squad1;
        private readonly Entity _squad2;

        public SquadMoveOrderSystemTests()
        {
            _sut = new SquadMoveOrderSystem();
            _testPool = new TestPool();
            _sut.SetPool(_testPool);

            _squad1 = _testPool.CreateEntity()
                .AddSquad(0, 1, 1)
                .AddMoveOrder(0, 1, 0);

            _squad2 = _testPool.CreateEntity()
                .AddSquad(1, 1, 1)
                .AddMoveOrder(0, 2, 0);
        }

        [Fact]
        public void TriggersOnAddedMoveOrder()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Squad, Matcher.MoveOrder));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void OrdersUnitsAccordingToSquadSettings()
        {
            _squad1.ReplaceSquad(0, 2, 2);
            var unit1 = CreateUnitWithSquadNumber(0);
            var unit2 = CreateUnitWithSquadNumber(0);
            var unit3 = CreateUnitWithSquadNumber(0);
            var unit4 = CreateUnitWithSquadNumber(0);

            _sut.Execute(_squad1.AsList());

            unit1.HasMoveOrderTo(new Vector(0, 1, 0));
            unit2.HasMoveOrderTo(new Vector(1, 1, 0));
            unit3.HasMoveOrderTo(new Vector(0, 1, 1));
            unit4.HasMoveOrderTo(new Vector(1, 1, 1));
        }

        [Fact]
        public void GivesUnitOrdersFromMultipleSquads()
        {
            var unit1 = CreateUnitWithSquadNumber(0);
            var unit2 = CreateUnitWithSquadNumber(1);

            _sut.Execute(new List<Entity>() { _squad1, _squad2});

            unit1.HasMoveOrderTo(new Vector(0, 1, 0));
            unit2.HasMoveOrderTo(new Vector(0, 2, 0));
        }

        [Fact]
        public void ReplacesOrderIfOneAlreadyExistsOnUnit()
        {
            var unit = CreateUnitWithSquadNumber(0).AddMoveOrder(1, 1, 1);

            _sut.Execute(_squad1.AsList());

            unit.HasMoveOrderTo(new Vector(0, 1, 0));
        }

        [Fact]
        public void DoesNotCrashWhenSquadDimensionIsZero()
        {
            CreateUnitWithSquadNumber(0).AddMoveOrder(1, 1, 1);
            _squad1.ReplaceSquad(0, 0, 0);

            _sut.Execute(_squad1.AsList());
        }

        [Fact]
        public void IgnoresDestroyedUnits()
        {
            var unit = CreateUnitWithSquadNumber(0).IsDestroy(true);

            _sut.Execute(_squad1.AsList());

            unit.hasMoveOrder.Should().BeFalse("destroyed unit should not get order");
        }

        private Entity CreateUnitWithSquadNumber(int squadNumber)
        {
            return _testPool.CreateEntity().AddUnit(squadNumber).AddPosition(0, 0, 0);
        }
    }
}
