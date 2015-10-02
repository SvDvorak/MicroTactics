using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class AiMoveOrderSystemTests : AiOrderTestsBase
    {
        private readonly AiMoveOrderSystem _sut;
        private readonly Entity _aiSquad1;
        private Entity _aiSquad2;

        public AiMoveOrderSystemTests()
        {
            _sut = new AiMoveOrderSystem();
            _sut.SetPool(Pool);
            _aiSquad1 = CreateAiAt(1, new Vector3(0, 0, 0));
            _aiSquad2 = CreateAiAt(20, new Vector3(5, 0, 3));
        }

        [Fact]
        public void DoesGiveAddMoveOrderIfNoEnemiesArePresent()
        {
            _sut.Execute();

            _aiSquad1.hasMoveOrder.Should().BeFalse("no enemy is present so no move order should be given");
        }

        [Fact]
        public void DoesNotGiveMoveOrderIfEnemyIsOutsideHalfOfSeeingRange()
        {
            _aiSquad1.ReplaceAi(1);

            CreateEnemyAt(0.6f, 0, 0);

            _sut.Execute();

            _aiSquad1.hasMoveOrder.Should().BeFalse("enemy is far away so no move order should be given");
        }

        [Fact]
        public void GivesDefaultMoveOrderWhenEnemyIsOnSamePosition()
        {
            CreateEnemyAt(0, 0, 0);

            _sut.Execute();

            _aiSquad1.ShouldHaveMoveOrderTo(new Vector3(1, 0, 0));
        }

        [Fact]
        public void MovesRemainingDistanceToMinimumEnemyDistance()
        {
            _aiSquad1
                .ReplaceAi(20)
                .ReplacePosition(4, 0, 0);

            CreateEnemyAt(5, 0, 0);

            _sut.Execute();

            _aiSquad1.ShouldHaveMoveOrderTo(new Vector3(-5, 0, 0));
            _aiSquad2.ShouldHaveMoveOrderTo(new Vector3(5, 0, 10));
        }
    }
}