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
            var attackingEntity = new TestEntity().AddPosition(10, 0, 0).AddRotation(1, 2, 3, 4).AddAttackOrder(3, 3, 3);

            _sut.Execute(attackingEntity.AsList());

            attackingEntity.hasFireArrow.Should().Be(true);
            var fireArrow = attackingEntity.fireArrow;
            fireArrow.Position.Should().Be(new Vector3(10, 2, 0));
            fireArrow.Rotation.Should().Be(new Quaternion(1, 2, 3, 4));
            fireArrow.Force.Should().Be(new Vector3(0, 0, 0));
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
}