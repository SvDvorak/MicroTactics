using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SelectSquadSystemTests
    {
        private readonly SelectSquadSystem _sut = new SelectSquadSystem();
        private readonly Pool _pool = new TestPool();

        public SelectSquadSystemTests()
        {
            _sut.SetPool(_pool);
        }

        [Fact]
        public void TriggersOnAddedInputDown()
        {
            _sut.trigger.Should().Be(Matcher.Input);
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void SetsSelectedOnClosestSquadInInputWhenPressingAndReleasing()
        {
            var squad1 = CreateSquad();
            var squad2 = CreateSquad();
            var selectionArea1 = CreateSelectionArea(squad1);
            var selectionArea2 = CreateSelectionArea(squad2);

            _sut.Execute(CreateInputPress(selectionArea2, selectionArea1).AsList());
            _sut.Execute(CreateInputRelease(selectionArea2, selectionArea1).AsList());

            squad1.isSelected.Should().BeTrue("first squad should have been selected");
            squad2.isSelected.Should().BeFalse("second squad shouldn't have been selected");
        }

        [Fact]
        public void DoesNothingIfNoSquadsAreInInput()
        {
            var squad = CreateSquad();

            _sut.Execute(CreateInputPress().AsList());

            squad.isSelected.Should().BeFalse("squad is not in selection so shouldn't become selected");
        }

        [Fact]
        public void DeselectsAllOtherSquadsWhenPressingANewSquad()
        {
            var squad1 = CreateSquad().IsSelected(true);
            var squad2 = CreateSquad().IsSelected(false);

            _sut.Execute(CreateInputPress(CreateSelectionArea(squad2)).AsList());

            squad1.isSelected.Should().BeFalse("first squad should have been deselected");
        }

        [Fact]
        public void DeselectsAllSquadsWhenReleasingOnNotASquad()
        {
            var squad1 = CreateSquad().IsSelected(true);

            _sut.Execute(CreateInputRelease(_pool.CreateEntity()).AsList());

            squad1.isSelected.Should().BeFalse("squad should have been deselected");
        }

        private Entity CreateSquad()
        {
            return _pool.CreateEntity().AddSquad(0);
        }

        private Entity CreateSelectionArea(Entity squad)
        {
            return _pool.CreateEntity().AddSelectionArea(squad);
        }

        private Entity CreateInputPress(params Entity[] hitEntities)
        {
            return _pool.CreateEntity().AddInput(InputState.Press, hitEntities.ToList());
        }

        private Entity CreateInputRelease(params Entity[] hitEntities)
        {
            return _pool.CreateEntity().AddInput(InputState.Release, hitEntities.ToList());
        }
    }
}