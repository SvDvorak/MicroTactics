using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features.CreateSquad
{
    public class SquadCreationSystemTests
    {
        private readonly SquadCreationSystem _sut;
        private readonly TestPool _pool;
        private readonly Entity _squad1 = new TestEntity().AddSquad(0).AddBoxFormation(0, 0, 0);
        private readonly Entity _squad2 = new TestEntity().AddSquad(1).AddBoxFormation(0, 0, 0);

        private List<Entity> Units { get { return _pool.GetEntities(Matcher.Unit).ToList(); } }

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
            Execute(_squad1);

            Units.Should().HaveCount(0);
        }

        [Fact]
        public void AddsMultipleUnitsAccordingToSquadSize()
        {
            _squad1.ReplaceBoxFormation(2, 2, 0);

            Execute(_squad1);

            Units.Should().HaveCount(4);
        }

        [Fact]
        public void SetsDestroyOnExistingUnitsWhenRecreatingSquad()
        {
            _squad1.ReplaceBoxFormation(2, 2, 0);
            Execute(_squad1);

            _squad1.ReplaceBoxFormation(1, 1, 0);
            Execute(_squad1);

            Units.Should().HaveCount(5);
            Units.Where(x => x.isDestroy && x.child.Value.isDestroy)
                .Should()
                .HaveCount(4, "all but one squad member should have been destroyed");
        }

        [Fact]
        public void HandlesMultipleSquads()
        {
            _squad1.ReplaceBoxFormation(1, 1, 1);
            _squad2.ReplaceBoxFormation(1, 1, 1);

            Execute(_squad1, _squad2);

            Units.Should().HaveCount(2);
        }

        [Fact]
        public void DoesNotTouchUnitsInOtherSquadsWhenRecreatingSquad()
        {
            Execute(_squad1.ReplaceBoxFormation(1, 1, 0));
            Execute(_squad2.ReplaceBoxFormation(1, 1, 0));

            Units.Should().HaveCount(2);
        }

        [Fact]
        public void AddsUnitAndResourceComponentToEachUnit()
        {
            Execute(_squad1.ReplaceBoxFormation(1, 1, 0));

            var unit = Units.SingleEntity();
            unit.ShouldHaveUnit(0);
            unit.ShouldHaveResource(Res.Unit);
        }

        [Fact]
        public void AddsPositionAndPlacesEachUnit()
        {
            Execute(_squad1.ReplaceBoxFormation(2, 2, 2));

            Units.First().ShouldHavePosition(-1, 0, -1);
            Units.Second().ShouldHavePosition(1, 0, -1);
            Units.Third().ShouldHavePosition(-1, 0, 1);
            Units.Fourth().ShouldHavePosition(1, 0, 1);
        }

        [Fact]
        public void CreatesSelectedIndicatorForEachUnit()
        {
            Execute(_squad1.ReplaceBoxFormation(1, 1, 0));

            var selectedIndicator = _pool.GetEntities(Matcher.Parent).SingleEntity();
            selectedIndicator.ShouldHaveParent(Units.SingleEntity());
            selectedIndicator.ShouldHaveResource(Res.SelectedIndicator);
        }

        [Fact]
        public void CreatesASelectionAreaForSquad()
        {
            Execute(_squad1);

            var selectionAreaEntity = _pool.GetEntities().SingleEntity();
            selectionAreaEntity.selectionArea.Parent.Should().Be(_squad1);
            selectionAreaEntity.resource.Name.Should().Be(Res.SelectionArea);
        }

        [Fact]
        public void RemovesExistingSelectionAreaWhenRecreating()
        {
            Execute(_squad1);
            Execute(_squad1);

            var nonDestroyedEntities = _pool.GetEntities().Where(x => !x.isDestroy).ToList();
            nonDestroyedEntities.Should().HaveCount(1);
            nonDestroyedEntities.Single().hasSelectionArea.Should().BeTrue("new selection area should have been created");
        }

        private void Execute(params Entity[] squads)
        {
            _sut.Execute(squads.ToList());
        }
    }

    public static class EntityExtensions
    {
        public static void ShouldHaveUnit(this Entity entity, int squadNumber)
        {
            entity.hasUnit.Should().BeTrue("entity should have a unit component");
            entity.unit.SquadNumber.Should().Be(squadNumber);
        }

        public static void ShouldHaveParent(this Entity child, Entity parent)
        {
            child.parent.Value.Should().Be(parent);
            parent.child.Value.Should().Be(child);
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