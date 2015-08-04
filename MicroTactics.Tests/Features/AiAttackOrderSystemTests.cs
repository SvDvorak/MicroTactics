using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class AiAttackOrderSystemTests
    {
        private readonly AiAttackOrderSystem _sut;
        private readonly TestPool _pool;

        public AiAttackOrderSystemTests()
        {
            _sut = new AiAttackOrderSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);
        }

        [Fact]
        public void GivesAttackOrderIfEnemyIsInRange()
        {
            CreateEnemyAt(1, 0, 0);
            var ai = CreateAiAt(2, new Vector(0, 0, 0));

            _sut.Execute();

            ai.hasAttackOrder.Should().BeTrue();
            ai.attackOrder.ShouldBeEquivalentTo(new Vector(1, 0, 0));
        }

        [Fact]
        public void FiresAtClosestEnemy()
        {
            CreateEnemyAt(0, 0, 3);
            CreateEnemyAt(0, 0, 2);
            CreateEnemyAt(0, 0, 1);
            var ai = CreateAiAt(5, new Vector(0, 0, 0));

            _sut.Execute();

            ai.attackOrder.ShouldBeEquivalentTo(new Vector(0, 0, 1));
        }

        [Fact]
        public void DoesNotGiveAttackOrderWhenNoEnemiesExist()
        {
            var ai = CreateAiAt(0, new Vector(0, 0, 0));

            _sut.Execute();

            ai.hasAttackOrder.Should().BeFalse("no enemies are available so shouldn't have attack order");
        }

        [Fact]
        public void DoesNotGiveAttackOrderWhenEnemyIsTooFarAway()
        {
            CreateEnemyAt(float.PositiveInfinity, 0, 0);
            var ai = CreateAiAt(0, new Vector(0, 0, 0));

            _sut.Execute();

            ai.hasAttackOrder.Should().BeFalse("enemy is too far away so shouldn't have attack order");
        }

        [Fact]
        public void RemovesPreviousAttackOrderIfNoEnemiesAreInRange()
        {
            CreateEnemyAt(float.PositiveInfinity, 0, 0);
            var ai = CreateAiAt(0, new Vector(0, 0, 0)).AddAttackOrder(0, 0, 0);

            _sut.Execute();

            ai.hasAttackOrder.Should().BeFalse("enemy is no longer in range so shouldn't have attack order");
        }

        private Entity CreateEnemyAt(float x, float y, float z)
        {
            return _pool.CreateEntity().AddPosition(x, y, z).IsEnemy(true);
        }

        private Entity CreateAiAt(float range, Vector vector)
        {
            return _pool.CreateEntity().AddPosition(vector.x, vector.y, vector.z).AddAi(range);
        }
    }
}