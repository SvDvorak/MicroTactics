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
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Arrow, Matcher.Physics, Matcher.Collision));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void RemovesPhysicsFromCollidedEntities()
        {
            var arrow = new TestEntity().AddArrow(new Vector3(), new Quaternion(), new Vector3()).AddPhysics(null);
            _sut.Execute(arrow.AsList());

            arrow.hasPhysics.Should().BeFalse("should have removed physics component");
        }
    }
}