using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SquadAttackOrderSystemTests
    {
        private readonly SquadAttackOrderSystem _sut;
        private readonly TestPool _pool;

        public SquadAttackOrderSystemTests()
        {
            _sut = new SquadAttackOrderSystem();
            _pool = new TestPool();
            _sut.SetPool(_pool);
        }

        [Fact]
        public void TriggersOnAddedAttackOrder()
        {
            _sut.trigger.Should().Be(Matcher.Unit);
            _sut.eventType.Should().Be(GroupEventType.OnEntityAdded);
        }

        [Fact]
        public void GivesAttackOrderAccordingToFormation()
        {
            var squad = _pool.CreateEntity().AddSquad(0).AddBoxFormation(2, 2, 2).AddAttackOrder(1, 0, 0);
            var unit1 = _pool.CreateEntity().AddUnit(0);
            var unit2 = _pool.CreateEntity().AddUnit(0);
            var unit3 = _pool.CreateEntity().AddUnit(0);
            var unit4 = _pool.CreateEntity().AddUnit(0);

            _sut.Execute(squad.AsList());

            unit1.HasAttackOrderTo(new Vector(0, 0, -1));
            unit2.HasAttackOrderTo(new Vector(2, 0, -1));
            unit3.HasAttackOrderTo(new Vector(0, 0, 1));
            unit4.HasAttackOrderTo(new Vector(2, 0, 1));
        }
    }
}