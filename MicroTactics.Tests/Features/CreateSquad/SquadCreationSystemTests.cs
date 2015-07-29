using System.Collections.Generic;
using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features.CreateSquad
{
    public class SquadCreationSystemTests
    {
        private readonly SquadCreationSystem _sut;
        private readonly TestPool _pool;
        private readonly TestEntity _squadEntity = new TestEntity();

        public SquadCreationSystemTests()
        {
            _sut = new SquadCreationSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);
        }

        [Fact]
        public void MatchesSquadComponents()
        {
            _sut.trigger.Should().Be(Matcher.Squad);
        }

        [Fact]
        public void ListensForEntitiesAdded()
        {
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void EmptySquadProducesNoUnits()
        {
            _squadEntity.AddSquad(0, 0);
            _sut.Execute(_squadEntity.AsList<Entity>());

            _pool.Count.Should().Be(0);
        }

        [Fact]
        public void AddsMultipleUnitsAccordingToSquadSize()
        {
            _squadEntity.AddSquad(2, 2);
            _sut.Execute(_squadEntity.AsList<Entity>());

            _pool.Count.Should().Be(4);
        }

        [Fact]
        public void RemovesExistingUnitsWhenRecreatingSquad()
        {
            _squadEntity.AddSquad(2, 2);
            _sut.Execute(_squadEntity.AsList<Entity>());
            _sut.Execute(_squadEntity.AsList<Entity>());

            _pool.Count.Should().Be(1);
        }
    }
}