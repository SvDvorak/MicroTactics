using Assets.Features.Move;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class RemoveMoveOrderSystemTests
    {
        private readonly RemoveMoveOrderSystem _sut;

        public RemoveMoveOrderSystemTests()
        {
            _sut = new RemoveMoveOrderSystem();
        }

        [Fact]
        public void TriggersOnAddedPositionAndMoveOrder()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Position, Matcher.MoveOrder));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void RemovesMoveOrderWhenHavingReachedPosition()
        {
            var entity = new TestEntity()
                .AddPosition(1, 1, 1)
                .AddMoveOrder(1, 1, 1);

            _sut.Execute(entity.AsList());

            entity.hasMoveOrder.Should().BeFalse("move order should have been removed");
        }

        [Fact]
        public void DoesNothingWhenNotHavingReachedPosition()
        {
            var entity = new TestEntity()
                .AddPosition(0, 0, 0)
                .AddMoveOrder(1, 1, 1);

            _sut.Execute(entity.AsList());

            entity.hasMoveOrder.Should().BeTrue("move order shouldn't have been removed");
        }
    }
}