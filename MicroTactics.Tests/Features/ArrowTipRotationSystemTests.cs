﻿using System;
using Assets;
using Entitas;
using FluentAssertions;
using Mono.GameMath;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class ArrowTipRotationSystemTests
    {
        private readonly TestPool _pool;
        private readonly PitchFromVelocitySystem _sut;

        public ArrowTipRotationSystemTests()
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
                .AddArrow(new Vector3(), Quaternion.Identity, new Vector3())
                .AddRotation(Quaternion.Identity)
                .AddPhysics(null)
                .AddVelocity(0, 0, 0);
        }
    }
}