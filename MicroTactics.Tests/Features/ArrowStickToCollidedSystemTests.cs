using System.Linq;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class ArrowStickToCollidedSystemTests
    {
        private readonly ArrowStickToCollidedSystem _sut;
        private readonly TestPool _pool;

        public ArrowStickToCollidedSystemTests()
        {
            _sut = new ArrowStickToCollidedSystem();
            _pool = new TestPool();
        }

        [Fact]
        public void TriggersOnAddedCollisionForPhysicalArrows()
        {
            _sut.trigger.Should().Be(Matcher.Collision.OnEntityAdded());
            _sut.ensureComponents.Should().Be(Matcher.AllOf(Matcher.Stickable, Matcher.Physics));
        }

        [Fact]
        public void AttachesToWhenRelativeVelocityIsHighEnough()
        {
            var stickToEntity = _pool.CreateEntity();
            var fastArrow = CreateCollidingArrow(stickToEntity, float.PositiveInfinity);
            var slowArrow = CreateCollidingArrow(new TestEntity(), 0);

            _sut.Execute(new[] { fastArrow, slowArrow }.ToList());

            fastArrow.ShouldAttachTo(stickToEntity);
            slowArrow.hasAttachTo.Should().BeFalse("slow arrow should not attach on collision");
        }

        [Fact]
        public void DoesNotAttachWhenOtherEntityIsNull()
        {
            var fastArrow = CreateCollidingArrow(null, float.PositiveInfinity);

            _sut.Execute(fastArrow.AsList());

            fastArrow.hasAttachTo.Should().BeFalse("should not attach when other entity is null");
        }

        [Fact]
        public void DoesNotAttachWhenArrowIsAlreadyAttached()
        {
            var collidedWith1 = _pool.CreateEntity();
            var arrow = CreateCollidingArrow(collidedWith1, float.PositiveInfinity);

            _sut.Execute(arrow.AsList());

            var collidedWith2 = _pool.CreateEntity();
            arrow.ReplaceCollision(collidedWith2, new Vector3(float.PositiveInfinity));

            _sut.Execute(arrow.AsList());

            arrow.ShouldAttachTo(collidedWith1);
        }

        private Entity CreateCollidingArrow(Entity collidedWith, float velocityMagnitude)
        {
            return SpawnHelper.Arrow(_pool)
                .AddPhysics(null)
                .AddCollision(collidedWith, new Vector3(velocityMagnitude, 0, 0))
                .ReplaceStickable(1);
        }
    }
}