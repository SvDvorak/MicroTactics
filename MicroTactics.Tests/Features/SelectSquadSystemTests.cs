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

        [Fact]
        public void TriggersOnAddedInputDown()
        {
            _sut.trigger.Should().Be(Matcher.InputPress);
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void SetsSelectedOnFirstSquadInInput()
        {
            var squad1 = CreateSquad();
            var squad2 = CreateSquad();
            var selectionArea1 = _pool.CreateEntity().AddSelectionArea(squad1);
            var selectionArea2 = _pool.CreateEntity().AddSelectionArea(squad2);

            _sut.Execute(CreateInputPress(selectionArea1, selectionArea2).AsList());

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

        private Entity CreateSquad()
        {
            return _pool.CreateEntity().AddSquad(0);
        }

        private Entity CreateInputPress(params Entity[] hitEntities)
        {
            return _pool.CreateEntity().AddInputPress(hitEntities.ToList());
        }
    }
}