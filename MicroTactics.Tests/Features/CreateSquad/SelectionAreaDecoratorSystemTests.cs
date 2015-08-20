using System.Linq;
using Assets.Features.CreateSquad;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features.CreateSquad
{
    public class SelectionAreaDecoratorSystemTests
    {
        private readonly SelectionAreaAddDecoratorSystem _sut = new SelectionAreaAddDecoratorSystem();
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
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Squad, Matcher.Player));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
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
            entity.child.Value.Should().Be(selectionAreaEntity);
            selectionAreaEntity.selectionArea.Parent.Should().Be(entity);
        }
    }

    public class SelectionAreaRemoveDecoratorSystemTests
    {
        private readonly SelectionAreaRemoveDecoratorSystem _sut = new SelectionAreaRemoveDecoratorSystem();
        private readonly Entity _squad1 = new TestEntity().AddSquad(0).AddChild(new TestEntity());
        private readonly Entity _squad2 = new TestEntity().AddSquad(1).AddChild(new TestEntity());

        [Fact]
        public void TriggersOnRemovedSquadOrPlayer()
        {
            _sut.triggers[0].Should().Be(Matcher.AllOf(Matcher.Destroy, Matcher.Squad, Matcher.Player));
            _sut.triggers[1].Should().Be(Matcher.AllOf(Matcher.Squad, Matcher.Player));
            _sut.eventTypes[0].Should().Be(GroupEventType.OnEntityAdded);
            _sut.eventTypes[1].Should().Be(GroupEventType.OnEntityRemoved);
        }

        [Fact]
        public void DestroysSelectionAreaWhenSquadIsDestroyed()
        {
            Execute(_squad1, _squad2);

            _squad1.child.Value.ShouldBeDestroyed(true);
            _squad2.child.Value.ShouldBeDestroyed(true);
        }

        private void Execute(params Entity[] squads)
        {
            _sut.Execute(squads.ToList());
        }
    }
}