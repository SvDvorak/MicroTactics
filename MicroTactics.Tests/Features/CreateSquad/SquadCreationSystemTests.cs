using System.Collections.Generic;
using System.Linq;
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
        public void ListensForAddedSquadOrFormationComponents()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Squad, Matcher.BoxFormation).OnEntityAdded());
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
            Units.Where(x => x.isDestroy && x.child.Value.First().isDestroy)
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
            selectedIndicator.ShouldBeHidden(true);
        }

        private void Execute(params Entity[] squads)
        {
            _sut.Execute(squads.ToList());
        }
    }
}