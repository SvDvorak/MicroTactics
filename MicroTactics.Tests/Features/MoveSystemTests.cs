using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class MoveSystemTests
    {
        [Fact]
        public void MovesEntitiesWithOrderAndPosition()
        {
            var pool = new TestPool();
            var entity = pool.CreateEntity()
                .AddPosition(0, 0, 0)
                .AddMoveOrder(1, 0, 0);

            var sut = new MoveSystem();
            sut.SetPool(pool);
            sut.Execute();

            entity.position.ShouldBeEquivalentTo(new Vector(1, 0, 0));
        }
    }
}