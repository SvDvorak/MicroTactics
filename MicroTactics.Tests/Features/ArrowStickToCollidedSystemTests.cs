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

        public ArrowStickToCollidedSystemTests()
        {
            _sut = new ArrowStickToCollidedSystem();
        }

        [Fact]
        public void TriggersOnAddedCollisionForPhysicalArrows()
        {
            _sut.trigger.Should().Be(Matcher.Collision.OnEntityAdded());
            _sut.ensureComponents.Should().Be(Matcher.AllOf(Matcher.Arrow, Matcher.Physics));
        }

        [Fact]
        public void SticksWhenRelativeVelocityIsHigh()
        {
            var stickToEntity = new TestEntity();
            var fastArrow = CreateCollidingArrow(stickToEntity, float.PositiveInfinity);
            var slowArrow = CreateCollidingArrow(new TestEntity(), 0);

            _sut.Execute(new[] { fastArrow, slowArrow }.ToList());

            fastArrow.hasPhysics.Should().BeFalse("should remove physics when collision is hard");
            slowArrow.hasPhysics.Should().BeTrue("should not remove physics when collision is soft");
            fastArrow.ShouldHaveParent(stickToEntity);
        }

        [Fact]
        public void DoesNotSetParentWhenOtherEntityIsNull()
        {
            var fastArrow = CreateCollidingArrow(null, float.PositiveInfinity);

            _sut.Execute(fastArrow.AsList());

            fastArrow.hasParent.Should().BeFalse("should not set parent when other entity is null");
        }

        private static Entity CreateCollidingArrow(TestEntity collidedWith, float velocityMagnitude)
        {
            return new TestEntity()
                .IsArrow(true)
                .AddCollision(collidedWith, new Vector3(velocityMagnitude, 0, 0))
                .AddPhysics(null);
        }
    }
}