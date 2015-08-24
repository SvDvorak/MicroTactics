using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class AiMoveOrderSystemTests
    {
        private readonly AiMoveOrderSystem _sut;
        private readonly TestPool _testPool;
        private readonly Entity _squad;

        public AiMoveOrderSystemTests()
        {
            _sut = new AiMoveOrderSystem();
            _testPool = new TestPool();
            _sut.SetPool(_testPool);
            _squad = _testPool.CreateEntity()
                .AddPosition(0, 0, 0)
                .AddSquad(0)
                .AddAi(1);
        }

        [Fact]
        public void DoesGiveAddMoveOrderIfNoEnemiesArePresent()
        {
            _sut.Execute();

            _squad.hasMoveOrder.Should().BeFalse("no enemy is present so no move order should be given");
        }

        [Fact]
        public void DoesNotGiveMoveOrderIfEnemyIsOutsideHalfOfSeeingRange()
        {
            _squad.ReplaceAi(1);

            _testPool.CreateEntity()
                .AddPosition(0.6f, 0, 0)
                .IsEnemy(true);

            _sut.Execute();

            _squad.hasMoveOrder.Should().BeFalse("enemy is far away so no move order should be given");
        }

        [Fact]
        public void GivesDefaultMoveOrderWhenEnemyIsOnSamePosition()
        {
            _testPool.CreateEntity()
                .AddPosition(0, 0, 0)
                .IsEnemy(true);

            _sut.Execute();

            _squad.ShouldHaveMoveOrderTo(new Vector3(1, 0, 0));
        }

        [Fact]
        public void MovesRemainingDistanceToMinimumEnemyDistance()
        {
            _squad
                .ReplaceAi(20)
                .ReplacePosition(4, 0, 0);

            var squad2 = _testPool.CreateEntity()
                .AddPosition(5, 0, 3)
                .AddSquad(1)
                .AddAi(20);

            _testPool.CreateEntity()
                .AddPosition(5, 0, 0)
                .IsEnemy(true);

            _sut.Execute();

            _squad.ShouldHaveMoveOrderTo(new Vector3(-5, 0, 0));
            squad2.ShouldHaveMoveOrderTo(new Vector3(5, 0, 10));
        }
    }
}