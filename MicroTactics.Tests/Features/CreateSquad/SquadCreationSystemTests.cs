using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features.CreateSquad
{
    public class SquadCreationSystemTests
    {
        private readonly SquadCreationSystem _sut;
        private readonly TestPool _pool;
        private readonly Entity _squad1 = new TestEntity().AddSquad(0).AddBoxFormation(0, 0, 0);
        private readonly Entity _squad2 = new TestEntity().AddSquad(0).AddBoxFormation(0, 0, 0);

        public SquadCreationSystemTests()
        {
            _sut = new SquadCreationSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);

            _pool.Count.Should().Be(0);
        }

        [Fact]
        public void MatchesSquadComponents()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Squad, Matcher.BoxFormation));
        }

        [Fact]
        public void ListensForEntitiesAdded()
        {
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void EmptySquadProducesNoUnits()
        {
            _sut.Execute(_squad1.AsList());

            _pool.Count.Should().Be(0);
        }

        [Fact]
        public void AddsMultipleUnitsAccordingToSquadSize()
        {
            _squad1.ReplaceBoxFormation(2, 2, 0);

            _sut.Execute(_squad1.AsList());

            _pool.Count.Should().Be(4);
        }

        [Fact]
        public void SetsDestroyOnExistingUnitsWhenRecreatingSquad()
        {
            _squad1.ReplaceBoxFormation(2, 2, 0);
            _sut.Execute(_squad1.AsList());

            _squad1.ReplaceBoxFormation(1, 1, 0);
            _sut.Execute(_squad1.AsList());

            var currentEntities = _pool.GetEntities();
            currentEntities.Should().HaveCount(5);
            currentEntities.Where(x => x.isDestroy).Should().HaveCount(4, "all units from first squad should be destroyed");
        }

        [Fact]
        public void DoesNotTouchUnitsInOtherSquadsWhenRecreatingSquad()
        {
            _sut.Execute(_squad1.ReplaceBoxFormation(1, 1, 0).AsList());
            _sut.Execute(_squad2.ReplaceBoxFormation(1, 1, 0).AsList());

            _pool.Count.Should().Be(2);
        }

        [Fact]
        public void AddsUnitAndMovementComponentToEachUnit()
        {
            _sut.Execute(_squad1.ReplaceBoxFormation(1, 1, 0).AsList());

            var createdEntity = _pool.GetEntities().SingleEntity();
            createdEntity.ShouldHaveUnit(0);
            createdEntity.ShouldHaveMovement(0.06f);
        }

        [Fact]
        public void AddsPositionAndPlacesEachUnit()
        {
            _sut.Execute(_squad1.ReplaceBoxFormation(2, 2, 2).AsList());

            var unit1 = _pool.GetEntities().First();
            var unit2 = _pool.GetEntities().Second();
            var unit3 = _pool.GetEntities().Third();
            var unit4 = _pool.GetEntities().Fourth();
            unit1.ShouldHavePosition(0, 0, 0);
            unit2.ShouldHavePosition(2, 0, 0);
            unit3.ShouldHavePosition(0, 0, 2);
            unit4.ShouldHavePosition(2, 0, 2);
        }
    }

    public static class EntityExtensions
    {
        public static void ShouldHaveUnit(this Entity entity, int squadNumber)
        {
            entity.hasUnit.Should().BeTrue("entity should have a unit component");
            entity.unit.SquadNumber.Should().Be(squadNumber);
        }

        public static void ShouldHaveMovement(this Entity entity, float moveSpeed)
        {
            entity.hasMovement.Should().BeTrue("entity should have movement component");
            entity.movement.ShouldBeEquivalentTo(new MovementComponent() { MoveSpeed = moveSpeed });
        }

        public static void ShouldHavePosition(this Entity entity, float x, float y, float z)
        {
            entity.hasPosition.Should().BeTrue("the entity should have a position component");
            entity.position.ShouldBeEquivalentTo(new Vector(x, y, z));
        }
    }
}