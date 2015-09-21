﻿using System.Linq;
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
            _sut.ensureComponents.Should().Be(Matcher.AllOf(Matcher.Stickable, Matcher.Physics));
        }

        [Fact]
        public void AttachesToWhenRelativeVelocityIsHighEnough()
        {
            var stickToEntity = new TestEntity();
            var fastArrow = CreateCollidingArrow(stickToEntity, float.PositiveInfinity);
            var slowArrow = CreateCollidingArrow(new TestEntity(), 0);

            _sut.Execute(new[] { fastArrow, slowArrow }.ToList());

            fastArrow.hasPhysics.Should().BeFalse("should remove physics when collision is hard");
            slowArrow.hasPhysics.Should().BeTrue("should not remove physics when collision is soft");
            fastArrow.ShouldAttachTo(stickToEntity);
        }

        [Fact]
        public void DoesNotAttachWhenOtherEntityIsNull()
        {
            var fastArrow = CreateCollidingArrow(null, float.PositiveInfinity);

            _sut.Execute(fastArrow.AsList());

            fastArrow.hasAttachTo.Should().BeFalse("should not attach when other entity is null");
        }

        private static Entity CreateCollidingArrow(TestEntity collidedWith, float velocityMagnitude)
        {
            return new TestEntity()
                .AddView(null)
                .AddCollision(collidedWith, new Vector3(velocityMagnitude, 0, 0))
                .AddStickable(1)
                .AddPhysics(null);
        }
    }
}