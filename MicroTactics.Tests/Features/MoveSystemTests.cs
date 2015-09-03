using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class MoveSystemTests
    {
        private readonly Entity _entity;
        private readonly MoveSystem _sut = new MoveSystem();

        public MoveSystemTests()
        {
            var pool = new TestPool();
            _entity = pool.CreateEntity()
                .AddPosition(0, 0, 0)
                .AddRotation(Quaternion.Identity)
                .AddMoveOrder(new Vector3(1, 0, 0), Quaternion.Identity)
                .AddMovement(0.2f);

            _sut.SetPool(pool);
        }

        [Fact]
        public void EntitiesWithPositionAndMovementMovesTowardsOrderPosition()
        {
            _sut.Execute();
            _sut.Execute();
            _sut.Execute();

            _entity.position.ToV3().Should().Be(new Vector3(0.6f, 0, 0));
        }

        [Fact]
        public void StopsWhenHavingReachedPosition()
        {
            _entity.ReplaceMovement(float.PositiveInfinity);
            var orderPosition = _entity.moveOrder.Position;

            _sut.Execute();

            _entity.position.ToV3().Should().Be(orderPosition);
        }

        [Fact]
        public void RotatesTowardsTarget()
        {
            _sut.Execute();

            _entity.rotation.ToQ().ShouldBeCloseTo(Quaternion.LookAt(Vector3.Right));
        }

        [Fact]
        public void DoesNotSetRotationWhenAlreadyStandingOnTarget()
        {
            _entity
                .ReplaceMoveOrder(Vector3.Zero, new Quaternion())
                .ReplacePosition(Vector3.Zero);

            _sut.Execute();

            _entity.rotation.ToQ().Should().Be(Quaternion.Identity);
        }
    }
}