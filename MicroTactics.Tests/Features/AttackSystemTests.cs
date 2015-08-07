using System.Linq;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class AttackSystemTests
    {
        private readonly AttackSystem _sut;

        public AttackSystemTests()
        {
            _sut = new AttackSystem();
        }

        [Fact]
        public void TriggersOnAddedPositionAndAttackOrder()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Position, Matcher.Rotation, Matcher.AttackOrder));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void AllEntitiesFire()
        {
            var attackingEntity1 = CreateAttackingEntity();
            var attackingEntity2 = CreateAttackingEntity();

            _sut.Execute(new [] { attackingEntity1, attackingEntity2 }.ToList());

            attackingEntity1.hasFireArrow.Should().Be(true);
            attackingEntity2.hasFireArrow.Should().Be(true);
        }

        [Fact]
        public void CalculatesPositionRotationAndForceToFire()
        {
            var lookingToTheRight = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.PiOver2);
            var positionToTheRight = new Vector3(20, 0, 0);
            var attackingEntity = new TestEntity().AddPosition(10, 0, 0).AddRotation(lookingToTheRight).AddAttackOrder(positionToTheRight);

            _sut.Execute(attackingEntity.AsList());

            attackingEntity.hasFireArrow.Should().Be(true);
            var fireArrow = attackingEntity.fireArrow;
            fireArrow.Position.Should().Be(new Vector3(10, 4, 0));
            fireArrow.Rotation.Should().Be(lookingToTheRight);
            fireArrow.Force.ShouldBeCloseTo(new Vector3(-263.8441f, 427.368f, 0));
        }

        [Fact]
        public void DoesNotFireArrowWhenOneIsAlreadyInProgress()
        {
            var attackingEntity = CreateAttackingEntity();
            attackingEntity.AddFireArrow(new Vector3(10, 10, 10), new Quaternion(), new Vector3());

            _sut.Execute(attackingEntity.AsList());

            attackingEntity.fireArrow.Position.Should().Be(new Vector3(10, 10, 10));
        }

        private static Entity CreateAttackingEntity()
        {
            return new TestEntity().AddPosition(0, 0, 0).AddRotation(0, 0, 0, 0).AddAttackOrder(0, 0, 0);
        }
    }

    public static class Vector3CloseToExtensions
    {
        private static float _minimumDifference = 0.0001f;

        public static void ShouldBeCloseTo(this Vector3 actual, Vector3 expected)
        {
            BeInRange(actual.X, expected.X);
            BeInRange(actual.Y, expected.Y);
            BeInRange(actual.Z, expected.Z);
        }

        private static void BeInRange(float actual, float expected)
        {
            actual.Should().BeInRange(expected - _minimumDifference, expected + _minimumDifference);
        }
    }
}