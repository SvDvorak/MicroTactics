using System;
using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class PitchFromVelocitySystemTests
    {
        private readonly TestPool _pool;
        private readonly PitchFromVelocitySystem _sut;

        public PitchFromVelocitySystemTests()
        {
            _sut = new PitchFromVelocitySystem();

            _pool = new TestPool();
            _sut.SetPool(_pool);
        }

        [Fact]
        public void AdjustForwardOfArrowToBeVelocity()
        {
            var arrow = CreateArrowEntity().ReplaceVelocity(0, 1, 0);

            _sut.Execute();

            var upRotation = Quaternion.CreateFromYawPitchRoll(0, MathHelper.PiOver2, 0);
            arrow.rotation.ToQ().Should().Be(upRotation);
        }

        [Fact]
        public void DoesNotRotateWhenVelocityIsZero()
        {
            var arrow = CreateArrowEntity().ReplaceVelocity(0, 0, 0);

            _sut.Execute();

            arrow.rotation.ToQ().Should().Be(Quaternion.Identity);
        }

        private Entity CreateArrowEntity()
        {
            return _pool.CreateEntity()
                .AddRotation(Quaternion.Identity)
                .AddPhysics(null)
                .AddVelocity(0, 0, 0);
        }
    }
}