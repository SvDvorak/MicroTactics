using Assets.Features.Move;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
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
        public void TriggersOnAddedPositionWithMoveOrder()
        {
            _sut.trigger.Should().Be(Matcher.Position.OnEntityAdded());
            _sut.ensureComponents.Should().Be(Matcher.MoveOrder);
        }

        [Fact]
        public void RemovesMoveOrderWhenHavingReachedPosition()
        {
            var entity = new TestEntity()
                .AddPosition(1, 1, 1)
                .AddMoveOrder(new Vector3(1, 1, 1), Quaternion.Identity);

            _sut.Execute(entity.AsList());

            entity.hasMoveOrder.Should().BeFalse("move order should have been removed");
        }

        [Fact]
        public void DoesNothingWhenNotHavingReachedPosition()
        {
            var entity = new TestEntity()
                .AddPosition(0, 0, 0)
                .AddMoveOrder(new Vector3(1, 1, 1), Quaternion.Identity);

            _sut.Execute(entity.AsList());

            entity.hasMoveOrder.Should().BeTrue("move order shouldn't have been removed");
        }
    }
}