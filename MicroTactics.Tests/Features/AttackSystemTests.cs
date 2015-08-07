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
        private TestPool _pool;

        public AttackSystemTests()
        {
            _sut = new AttackSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);
        }

        [Fact]
        public void TriggersOnAddedPositionAndAttackOrder()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.Position, Matcher.Rotation, Matcher.AttackOrder, Matcher.ArrowTemplate));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void AllEntitiesFire()
        {
            _sut.Execute(new [] { CreateAttackingEntity(), CreateAttackingEntity() }.ToList());

            var poolEntities = _pool.GetEntities();
            poolEntities.First().hasArrow.Should().Be(true);
            poolEntities.Second().hasArrow.Should().Be(true);
        }

        [Fact]
        public void CalculatesPositionRotationAndForceToFire()
        {
            var lookingToTheRight = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.PiOver2);
            var positionToTheRight = new Vector3(20, 0, 0);
            var attackingEntity = new TestEntity()
                .AddPosition(10, 0, 0)
                .AddRotation(lookingToTheRight)
                .AddAttackOrder(positionToTheRight)
                .AddArrowTemplate(null);

            _sut.Execute(attackingEntity.AsList());

            var arrowEntity = _pool.GetEntities().SingleEntity();
            arrowEntity.hasArrow.Should().Be(true);

            var arrow = arrowEntity.arrow;
            arrow.Position.Should().Be(new Vector3(10, 4, 0));
            arrow.Rotation.Should().Be(lookingToTheRight);
            arrow.Force.ShouldBeCloseTo(new Vector3(-263.8441f, 427.368f, 0));
        }

        [Fact]
        public void AddsViewTemplateToArrow()
        {
            var attackingEntity = CreateAttackingEntity();

            _sut.Execute(attackingEntity.AsList());

            attackingEntity.hasArrowTemplate.Should().BeTrue("arrow should have arrow template");
        }

        [Fact]
        public void RemovesAttackOrderAfterHavingFired()
        {
            var attackingEntity = CreateAttackingEntity();
            _sut.Execute(attackingEntity.AsList());

            attackingEntity.hasAttackOrder.Should().BeFalse("should not have attack order after firing");
        }

        private static Entity CreateAttackingEntity()
        {
            return new TestEntity().AddPosition(0, 0, 0).AddRotation(0, 0, 0, 0).AddAttackOrder(0, 0, 0).AddArrowTemplate(null);
        }
    }
}