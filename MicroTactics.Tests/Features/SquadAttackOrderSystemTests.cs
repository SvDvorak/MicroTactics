using System.Linq;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SquadAttackOrderSystemTests
    {
        private readonly SquadAttackOrderSystem _sut;

        public SquadAttackOrderSystemTests()
        {
            _sut = new SquadAttackOrderSystem();
        }

        [Fact]
        public void TriggersOnAddedAttackOrderWithUnitsCacheAndFormation()
        {
            _sut.trigger.Should().Be(Matcher.AttackOrder);
            _sut.ensureComponents.Should().Be(Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void GivesAttackOrderAccordingToFormation()
        {
            var unit1 = CreateUnit();
            var unit2 = CreateUnit();
            var unit3 = CreateUnit();
            var unit4 = CreateUnit();

            var squad = new TestEntity()
                .AddSquad(0)
                .AddUnitsCache(new [] { unit1, unit2, unit3, unit4 }.ToList())
                .AddBoxFormation(2, 2, 2)
                .AddAttackOrder(1, 0, 0);

            _sut.Execute(squad.AsList());

            unit1.HasAttackOrderTo(new Vector3(0, 0, -1));
            unit2.HasAttackOrderTo(new Vector3(2, 0, -1));
            unit3.HasAttackOrderTo(new Vector3(0, 0, 1));
            unit4.HasAttackOrderTo(new Vector3(2, 0, 1));
        }

        [Fact]
        public void HandlesMultipleSquads()
        {
            var unit1 = CreateUnit(0);
            var unit2 = CreateUnit(1);

            var squad1 = new TestEntity()
                .AddSquad(0)
                .AddUnitsCache(unit1.AsList())
                .AddBoxFormation(1, 1, 1)
                .AddAttackOrder(1, 0, 0);

            var squad2 = new TestEntity()
                .AddSquad(1)
                .AddUnitsCache(unit2.AsList())
                .AddBoxFormation(1, 1, 1)
                .AddAttackOrder(0, 1, 0);

            _sut.Execute(new [] { squad1, squad2 }.ToList());

            unit1.HasAttackOrderTo(new Vector3(1, 0, 0));
            unit2.HasAttackOrderTo(new Vector3(0, 1, 0));
        }

        [Fact]
        public void ReplacesExistingOrder()
        {
            var unit = CreateUnit().AddAttackOrder(0, 0, 1);

            var squad = new TestEntity()
                .AddSquad(0)
                .AddUnitsCache(unit.AsList())
                .AddBoxFormation(1, 1, 1)
                .AddAttackOrder(1, 0, 0);

            _sut.Execute(squad.AsList());

            unit.HasAttackOrderTo(new Vector3(1, 0, 0));
        }

        private static Entity CreateUnit(int squadNumber = 0)
        {
            return new TestEntity().AddUnit(squadNumber);
        }
    }
}