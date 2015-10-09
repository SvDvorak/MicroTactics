using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class DestroySystemTests
    {
        [Fact]
        public void TriggersOnAddedDestroyComponent()
        {
            var sut = new DestroySystem();

            sut.trigger.Should().Be(Matcher.Destroy.OnEntityAdded());
        }

        [Fact]
        public void DestroysAllEntitiesWithDestroyComponent()
        {
            var sut = new DestroySystem();

            var testPool = new TestPool();
            testPool.CreateEntity();
            var destroyEntity = testPool.CreateEntity();

            sut.SetPool(testPool);
            sut.Execute(destroyEntity.AsList());

            testPool.count.Should().Be(1, "pool should only contain non-destroyed entities after running system");
        }
    }
}
