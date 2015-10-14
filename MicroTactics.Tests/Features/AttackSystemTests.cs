using System.Linq;
using Assets;
using Entitas;
using FluentAssertions;
using UnityEngine;
using Xunit;
using Quaternion = Mono.GameMath.Quaternion;
using Random = Assets.Random;
using Vector3 = Mono.GameMath.Vector3;

namespace MicroTactics.Tests.Features
{
    public class AttackSystemTests
    {
        private readonly AttackSystem _sut;
        private readonly TestPool _pool;
        private readonly Entity _attackingEntity = CreateAttackingEntity();
        private const float NoArrowVariationRandom = 0.5f;

        public AttackSystemTests()
        {
            _sut = new AttackSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);
            SetRandom(NoArrowVariationRandom);
        }

        [Fact]
        public void TriggersOnAddedAttackOrderWithPositionAndRotation()
        {
            _sut.trigger.Should().Be(Matcher.AttackOrder.OnEntityAdded());
            _sut.ensureComponents.Should().Be(Matcher.AllOf(Matcher.Unit, Matcher.Position, Matcher.Rotation));
        }

        [Fact]
        public void AllEntitiesFireArrows()
        {
            _sut.Execute(new [] { CreateAttackingEntity(), CreateAttackingEntity() }.ToList());

            var poolEntities = _pool.GetEntities();
            poolEntities.First().ShouldHaveResource("Arrow");
            poolEntities.Second().ShouldHaveResource("Arrow");
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

            var arrowEntity = PoolEntity();
            arrowEntity.position.ToV3().ShouldBeCloseTo(new Vector3(10, 4, 0));
            arrowEntity.rotation.ToQ().ShouldBeCloseTo(lookingToTheRight);
            arrowEntity.force.ToV3().ShouldBeCloseTo(new Vector3(263.8441f, 427.368f, 0));
        }

        [Fact]
        public void AddsARandomVariationWhenFiringArrow()
        {
            SetRandom(1);
            _attackingEntity
                .ReplaceAttackOrder(new Vector3(10, 0, 0));

            _sut.Execute(_attackingEntity.AsList());

            PoolEntity().force.ToV3().ShouldBeCloseTo(new Vector3(263.1658f, 427.225f, 11.49776f));
        }

        [Fact]
        public void AddsDelayedDestroyToArrow()
        {
            _sut.Execute(_attackingEntity.AsList());

            PoolEntity().ShouldHaveDelayedDestroy(20*Simulation.FrameRate);
        }

        [Fact]
        public void ArrowShouldStickOnHit()
        {
            _sut.Execute(_attackingEntity.AsList());

            PoolEntity().ShouldBeStickable(10);
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

        private static void SetRandom(float value)
        {
            var random = new TestRandom { Value = value };
            Random.Instance = random;
        }

        private Entity PoolEntity()
        {
            return _pool.GetEntities().SingleEntity();
        }

        private static Entity CreateAttackingEntity()
        {
            return new TestEntity().AddPosition(0, 0, 0).AddRotation(0, 0, 0, 0).AddAttackOrder(0, 0, 0);
        }
    }
}