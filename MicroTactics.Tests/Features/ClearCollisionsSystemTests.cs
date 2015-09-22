using System.Linq;
using Assets.Features.Attack;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class ClearCollisionsSystemTests
    {
        private readonly ClearCollisionsSystem _sut = new ClearCollisionsSystem();

        [Fact]
        public void TriggersOnAddedCollision()
        {
            _sut.trigger.Should().Be(Matcher.Collision.OnEntityAdded());
        }

        [Fact]
        public void RemovesCollisionsFromEntities()
        {
            var colliding1 = CreateCollidingEntity();
            var colliding2 = CreateCollidingEntity();

            _sut.Execute(new [] { colliding1, colliding2 }.ToList());

            ShouldNotHaveCollision(colliding1);
            ShouldNotHaveCollision(colliding2);
        }

        private static void ShouldNotHaveCollision(Entity colliding)
        {
            colliding.hasCollision.Should().BeFalse(colliding + " should have removed collision");
        }

        private static Entity CreateCollidingEntity()
        {
            return new TestEntity().AddCollision(null, Vector3.Zero);
        }
    }
}
