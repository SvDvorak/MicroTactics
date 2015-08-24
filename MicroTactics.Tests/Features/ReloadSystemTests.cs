using Assets.Features.Attack;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class ReloadSystemTests
    {
        private readonly ReloadSystem _sut = new ReloadSystem();
        private readonly TestPool _pool = new TestPool();

        public ReloadSystemTests()
        {
            _sut.SetPool(_pool);
        }

        [Fact]
        public void CountsDownReloadFrames()
        {
            var reloadingEntity1 = _pool.CreateEntity().AddReload(2);
            var reloadingEntity2 = _pool.CreateEntity().AddReload(3);

            _sut.Execute();

            reloadingEntity1.reload.FramesLeft.Should().Be(1);
            reloadingEntity2.reload.FramesLeft.Should().Be(2);
        }

        [Fact]
        public void DoesNothingToNonReloadables()
        {
            var nonReloadingEntity = _pool.CreateEntity();

            _sut.Execute();

            nonReloadingEntity.hasReload.Should().BeFalse();
        }

        [Fact]
        public void RemovesReloadWhenFramesLeftAreZero()
        {
            var reloadingEntity = _pool.CreateEntity().AddReload(1);

            _sut.Execute();

            reloadingEntity.hasReload.Should().BeFalse("reload should be removed when no frames are left");
        }
    }
}