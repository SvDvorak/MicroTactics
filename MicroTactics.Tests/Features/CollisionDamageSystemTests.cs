using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Features.Attack;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class CollisionDamageSystemTests
    {
        private readonly CollisionDamageSystem _sut;
        private readonly TestEntity _collidedWith;

        public CollisionDamageSystemTests()
        {
            _sut = new CollisionDamageSystem();
            _collidedWith = new TestEntity();
        }

        [Fact]
        public void TriggersOnHealthEntitiesWithCollisions()
        {
            _sut.trigger.Should().Be(Matcher.Collision.OnEntityAdded());
            _sut.ensureComponents.Should().Be(Matcher.Health);
        }

        [Fact]
        public void DecreasesHealthByCollisionVelocity()
        {
            var healthEntity1 = CreateHealthyEntityWithCollision(1);
            var healthEntity2 = CreateHealthyEntityWithCollision(10);

            _sut.Execute(new [] { healthEntity1, healthEntity2 }.ToList());

            healthEntity1.ShouldHaveHealth(99);
            healthEntity2.ShouldHaveHealth(90);
        }

        [Fact]
        public void SetsEntityAsDeadWhenHealthIsBelowZero()
        {
            var dyingEntity = CreateHealthyEntityWithCollision(float.PositiveInfinity);

            _sut.Execute(dyingEntity.AsList());

            dyingEntity.ShouldBeDestroyed(true);
            dyingEntity.isLeavingBody.Should().BeTrue("should leave body when dead");
        }

        private Entity CreateHealthyEntityWithCollision(float collisionVelocity)
        {
            return new TestEntity().AddHealth(100).AddCollision(_collidedWith, new Vector3(collisionVelocity, 0, 0));
        }
    }
}
