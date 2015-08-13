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
        private readonly MoveSystem _sut;

        public MoveSystemTests()
        {
            var pool = new TestPool();
            _entity = pool.CreateEntity()
                .AddPosition(0, 0, 0)
                .AddMoveOrder(new Vector3(1, 0, 0), Quaternion.Identity)
                .AddMovement(0.2f);

            _sut = new MoveSystem();
            _sut.SetPool(pool);
        }

        [Fact]
        public void EntitiesWithPositionAndMovementMovesTowardsOrderPosition()
        {
            _sut.Execute();
            _sut.Execute();
            _sut.Execute();

            _entity.position.ToV3().ShouldBeEquivalentTo(new Vector3(0.6f, 0, 0));
        }

        [Fact]
        public void StopsWhenHavingReachedPosition()
        {
            _entity.ReplaceMovement(float.PositiveInfinity);

            _sut.Execute();

            _entity.position.ToV3().ShouldBeEquivalentTo(_entity.moveOrder.Position);
        }
    }
}