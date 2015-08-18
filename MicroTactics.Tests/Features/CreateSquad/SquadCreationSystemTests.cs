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

        private List<Entity> UnitsInPool { get { return _pool.GetEntities().Where(x => x.hasUnit).ToList(); } }

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

            UnitsInPool.Should().HaveCount(0);
        }

        [Fact]
        public void AddsMultipleUnitsAccordingToSquadSize()
        {
            _squad1.ReplaceBoxFormation(2, 2, 0);

            _sut.Execute(_squad1.AsList());

            UnitsInPool.Should().HaveCount(4);
        }

        [Fact]
        public void SetsDestroyOnExistingUnitsWhenRecreatingSquad()
        {
            _squad1.ReplaceBoxFormation(2, 2, 0);
            _sut.Execute(_squad1.AsList());

            _squad1.ReplaceBoxFormation(1, 1, 0);
            _sut.Execute(_squad1.AsList());

            UnitsInPool.Should().HaveCount(5);
            UnitsInPool.Where(x => x.isDestroy).Should().HaveCount(4, "all units from first squad should be destroyed");
        }

        [Fact]
        public void HandlesMultipleSquads()
        {
            _squad1.ReplaceBoxFormation(1, 1, 1);
            _squad2.ReplaceBoxFormation(1, 1, 1);

            _sut.Execute(new [] { _squad1, _squad2 }.ToList());

            UnitsInPool.Should().HaveCount(2);
        }

        [Fact]
        public void DoesNotTouchUnitsInOtherSquadsWhenRecreatingSquad()
        {
            _sut.Execute(_squad1.ReplaceBoxFormation(1, 1, 0).AsList());
            _sut.Execute(_squad2.ReplaceBoxFormation(1, 1, 0).AsList());

            UnitsInPool.Should().HaveCount(2);
        }

        [Fact]
        public void AddsUnitMovementAndResourceComponentToEachUnit()
        {
            _sut.Execute(_squad1.ReplaceBoxFormation(1, 1, 0).AsList());

            var createdEntity = UnitsInPool.SingleEntity();
            createdEntity.ShouldHaveUnit(0);
            createdEntity.ShouldHaveMovement(0.06f);
            createdEntity.ShouldHaveResource("Unit");
        }

        [Fact]
        public void AddsPositionAndPlacesEachUnit()
        {
            _sut.Execute(_squad1.ReplaceBoxFormation(2, 2, 2).AsList());

            UnitsInPool.First().ShouldHavePosition(-1, 0, -1);
            UnitsInPool.Second().ShouldHavePosition(1, 0, -1);
            UnitsInPool.Third().ShouldHavePosition(-1, 0, 1);
            UnitsInPool.Fourth().ShouldHavePosition(1, 0, 1);
        }

        [Fact]
        public void CreatesASelectionAreaForSquad()
        {
            _sut.Execute(_squad1.AsList());

            var selectionAreaEntity = _pool.GetEntities().SingleEntity();
            selectionAreaEntity.selectionArea.Parent.Should().Be(_squad1);
            selectionAreaEntity.resource.Name.Should().Be(Res.SelectionArea);
        }

        [Fact]
        public void RemovesExistingSelectionAreaWhenRecreating()
        {
            _sut.Execute(_squad1.AsList());
            _sut.Execute(_squad1.AsList());

            var nonDestroyedEntities = _pool.GetEntities().Where(x => !x.isDestroy);
            nonDestroyedEntities.Should().HaveCount(1);
            nonDestroyedEntities.Single().hasSelectionArea.Should().BeTrue("new selection area should have been created");
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
            entity.position.ShouldBeEquivalentTo(new VectorClass(x, y, z));
        }

        public static void ShouldHaveResource(this Entity entity, string resource)
        {
            entity.hasResource.Should().BeTrue("the entity should have a resource component");
            entity.resource.Name.Should().Be(resource);
        }
    }
}