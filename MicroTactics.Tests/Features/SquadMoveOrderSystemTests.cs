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
        private readonly Entity _squad1;
        private readonly Entity _squad2;

        public SquadMoveOrderSystemTests()
        {
            _sut = new SquadMoveOrderSystem();

            _squad1 = new TestEntity()
                .AddSquad(0)
                .AddBoxFormation(1, 1, 0)
                .AddMoveOrder(0, 1, 0);

            _squad2 = new TestEntity()
                .AddSquad(1)
                .AddBoxFormation(1, 1, 0)
                .AddMoveOrder(0, 2, 0);
        }

        [Fact]
        public void TriggersOnAddedMoveOrder()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation, Matcher.MoveOrder));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void OrdersUnitsAccordingToFormation()
        {
            var unit1 = CreateUnit(0);
            var unit2 = CreateUnit(0);
            var unit3 = CreateUnit(0);
            var unit4 = CreateUnit(0);

            _squad1
                .ReplaceBoxFormation(2, 2, 2)
                .ReplaceUnitsCache(new [] { unit1, unit2, unit3, unit4 }.ToList());

            _sut.Execute(_squad1.AsList());

            unit1.HasMoveOrderTo(new VectorClass(-1, 1, -1));
            unit2.HasMoveOrderTo(new VectorClass(1, 1, -1));
            unit3.HasMoveOrderTo(new VectorClass(-1, 1, 1));
            unit4.HasMoveOrderTo(new VectorClass(1, 1, 1));
        }

        [Fact]
        public void GivesUnitOrdersFromMultipleSquads()
        {
            var unit1 = CreateUnit(0);
            var unit2 = CreateUnit(1);
            _squad1.ReplaceUnitsCache(unit1.AsList());
            _squad2.ReplaceUnitsCache(unit2.AsList());

            _sut.Execute(new List<Entity>() { _squad1, _squad2});

            unit1.HasMoveOrderTo(new VectorClass(0, 1, 0));
            unit2.HasMoveOrderTo(new VectorClass(0, 2, 0));
        }

        [Fact]
        public void ReplacesOrderIfOneAlreadyExistsOnUnit()
        {
            var unit = CreateUnit(0).AddMoveOrder(1, 1, 1);
            _squad1.ReplaceUnitsCache(unit.AsList());

            _sut.Execute(_squad1.AsList());

            unit.HasMoveOrderTo(new VectorClass(0, 1, 0));
        }

        private Entity CreateUnit(int squadNumber)
        {
            return new TestEntity().AddUnit(squadNumber).AddPosition(0, 0, 0);
        }
    }
}
