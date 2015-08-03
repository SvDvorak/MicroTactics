using Entitas;
using FluentAssertions;
using Xunit;

namespace MicroTactics.Tests.Features
{
    public class SquadCenterPositionSystemTests
    {
        private readonly SquadCenterPositionSystem _sut;
        private readonly TestPool _pool;
        private int _squadCounter;

        public SquadCenterPositionSystemTests()
        {
            _sut = new SquadCenterPositionSystem();
            _pool = new TestPool();
        }

        [Fact]
        public void UsesAveragePositionFromAllUnitsAsPosition()
        {
            var squad = CreateSquadAt(0, 0, 0);
            CreateUnitForSquadAt(squad, 1, 0, 0);
            CreateUnitForSquadAt(squad, 2, 0, 0);

            _sut.SetPool(_pool);
            _sut.Execute();

            squad.position.ShouldBeEquivalentTo(new Vector(1.5f, 0, 0));
        }

        [Fact]
        public void SetsForMultipleSquads()
        {
            var squad1 = CreateSquadAt(0, 0, 0);
            var squad2 = CreateSquadAt(0, 0, 0);
            CreateUnitForSquadAt(squad1, 1, 0, 0);
            CreateUnitForSquadAt(squad2, 2, 0, 0);

            _sut.SetPool(_pool);
            _sut.Execute();

            squad1.position.ShouldBeEquivalentTo(new Vector(1, 0, 0));
            squad2.position.ShouldBeEquivalentTo(new Vector(2, 0, 0));
        }

        private Entity CreateSquadAt(float x, float y, float z)
        {
            return _pool.CreateEntity().AddSquad(_squadCounter++).AddPosition(x, y, z);
        }

        private void CreateUnitForSquadAt(Entity squadEntity, int x, int y, int z)
        {
            _pool.CreateEntity().AddUnit(squadEntity.squad.Number).AddPosition(x, y, z);
        }
    }
}