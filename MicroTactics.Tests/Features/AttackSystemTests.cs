using System.Linq;
using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class AttackSystemTests
    {
        private readonly AttackSystem _sut;
        private readonly TestPool _pool;
        private readonly Entity _attackingEntity = CreateAttackingEntity();

        public AttackSystemTests()
        {
            _sut = new AttackSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);
        }

        [Fact]
        public void TriggersOnAddedAttackOrderWithPositionAndRotation()
        {
            _sut.trigger.Should().Be(Matcher.AllOf(Matcher.AttackOrder));
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
            _sut.ensureComponents.Should().Be(Matcher.AllOf(Matcher.Unit, Matcher.Position, Matcher.Rotation));
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
            var lookingToTheRight = Quaternion.LookAt(Vector3.Right);
            var positionToTheRight = new Vector3(20, 0, 0);
            _attackingEntity
                .ReplacePosition(10, 0, 0)
                .ReplaceRotation(lookingToTheRight)
                .ReplaceAttackOrder(positionToTheRight);

            _sut.Execute(_attackingEntity.AsList());

            var arrowEntity = GetSingleArrow();
            var expectedPosition = new Vector3(10, 4, 0);
            var expectedForce = new Vector3(263.8441f, 427.368f, 0);
            arrowEntity.arrow.Position.ShouldBeCloseTo(expectedPosition);
            arrowEntity.arrow.Rotation.ShouldBeCloseTo(lookingToTheRight);
            arrowEntity.arrow.Force.ShouldBeCloseTo(expectedForce);

            arrowEntity.position.ToV3().ShouldBeCloseTo(expectedPosition);
            arrowEntity.rotation.ToQ().ShouldBeCloseTo(lookingToTheRight);
        }

        [Fact]
        public void AddsResourceToArrow()
        {
            _sut.Execute(_attackingEntity.AsList());

            GetSingleArrow().hasResource.Should().BeTrue("arrow should have resource");
            GetSingleArrow().resource.Name.Should().Be("Arrow");
        }

        [Fact]
        public void RemovesAttackOrderAfterHavingFired()
        {
            _sut.Execute(_attackingEntity.AsList());

            _attackingEntity.hasAttackOrder.Should().BeFalse("should not have attack order after firing");
        }

        [Fact]
        public void RotatesTowardsTarget()
        {
            _attackingEntity.ReplaceAttackOrder(10, 0, 0);

            _sut.Execute(_attackingEntity.AsList());

            _attackingEntity.rotation.ToQ().ShouldBeCloseTo(Quaternion.LookAt(Vector3.Right));
        }

        [Fact]
        public void StartsReloadingAfterFiring()
        {
            _sut.Execute(_attackingEntity.AsList());

            _attackingEntity.hasReload.Should().BeTrue();
            _attackingEntity.reload.FramesLeft.Should().Be(5 * Simulation.FrameRate);
        }

        [Fact]
        public void DoesNotFireIfAlreadyReloading()
        {
            _attackingEntity.AddReload(0);

            _sut.Execute(_attackingEntity.AsList());

            _pool.GetEntities().Should().HaveCount(0);
        }

        private Entity GetSingleArrow()
        {
            var arrowEntity = _pool.GetEntities().SingleEntity();
            arrowEntity.hasArrow.Should().Be(true);
            return arrowEntity;
        }

        private static Entity CreateAttackingEntity()
        {
            return new TestEntity().AddPosition(0, 0, 0).AddRotation(0, 0, 0, 0).AddAttackOrder(0, 0, 0);
        }
    }
}