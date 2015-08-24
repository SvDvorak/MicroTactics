using Assets.Features.Destruction;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class DelayedDestroySystemTests
    {
        private readonly TestPool _pool = new TestPool();
        private readonly DelayedDestroySystem _sut = new DelayedDestroySystem();

        public DelayedDestroySystemTests()
        {
            _sut.SetPool(_pool);
        }

        [Fact]
        public void CountsDownDestroyDelayFrames()
        {
            var toDestroy = _pool.CreateEntity().AddDelayedDestroy(10);

            _sut.Execute();

            toDestroy.ShouldHaveDelayedDestroy(9);
        }

        [Fact]
        public void DoesNothingToNonDestroyDelayedEntities()
        {
            var notToBeDestroyed = _pool.CreateEntity();

            _sut.Execute();

            notToBeDestroyed.hasDelayedDestroy.Should().BeFalse();
        }

        [Fact]
        public void RemovesDelayAndSetsDestroyWhenFramesLeftAreZero()
        {
            var toDestroy = _pool.CreateEntity().AddDelayedDestroy(1);

            _sut.Execute();

            toDestroy.hasDelayedDestroy.Should().BeFalse("destroy delay should be removed when no frames are left");
            toDestroy.ShouldBeDestroyed(true);
        }
    }
}