using System.Linq;
using Assets.Features.CreateSquad;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features.CreateSquad
{
    public class SelectionAreaDecoratorSystemTests
    {
        private readonly SelectionAreaDecoratorSystem _sut = new SelectionAreaDecoratorSystem();
        private readonly TestPool _pool = new TestPool();
        private readonly Entity _squad1 = new TestEntity().AddSquad(0);
        private readonly Entity _squad2 = new TestEntity().AddSquad(1);

        public SelectionAreaDecoratorSystemTests()
        {
            _sut.SetPool(_pool);
        }

        [Fact]
        public void TriggersOnAddedSquadOrPlayer()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Squad, Matcher.Player).OnEntityAdded());
        }

        [Fact]
        public void CreatesASelectionAreaForSquad()
        {
            Execute(_squad1, _squad2);

            var squad1SelectionArea = _pool.GetEntities()[0];
            squad1SelectionArea.ShouldBeSelectionAreaFor(_squad1);
            squad1SelectionArea.ShouldHaveResource(Res.SelectionArea);

            var squad2SelectionArea = _pool.GetEntities()[1];
            squad2SelectionArea.ShouldBeSelectionAreaFor(_squad2);
            squad2SelectionArea.ShouldHaveResource(Res.SelectionArea);
        }

        private void Execute(params Entity[] squads)
        {
            _sut.Execute(squads.ToList());
        }
    }

    public static class EntityExtensions
    {
        public static void ShouldBeSelectionAreaFor(this Entity selectionAreaEntity, Entity entity)
        {
            entity.children.Value.ShouldAllBeEquivalentTo(selectionAreaEntity.AsList());
            selectionAreaEntity.selectionArea.Parent.Should().Be(entity);
        }
    }
}