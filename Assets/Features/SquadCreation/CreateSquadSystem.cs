using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;
using Vexe.Runtime.Extensions;

public class CreateSquadSystem : IReactiveSystem, ISetPool
{
    private Group _unitsGroup;
    private Pool _pool;

    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Squad, Matcher.BoxFormation).OnEntityAdded(); } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
        _unitsGroup = pool.GetGroup(Matcher.Unit);
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var squadEntity in entities)
        {
            RemoveExistingUnitsFromSquad(squadEntity.squad.Number);
            CreateUnitsForSquad(squadEntity.squad, squadEntity.boxFormation);
        }
    }

    private void CreateUnitsForSquad(SquadComponent squad, BoxFormationComponent formation)
    {
        for (var i = 0; i < formation.Columns * formation.Rows; i++)
        {
            var position = UnitInSquadPositioner.GetPosition(formation, i);
            var unit = SpawnHelper.Unit(_pool, squad.Number, position);

            unit.AddChildTwoWay(SpawnHelper.SelectedIndicator(_pool));
        }
    }

    private void RemoveExistingUnitsFromSquad(int squadNumber)
    {
        var unitsInSquad = _unitsGroup.GetEntities().Where(x => x.unit.SquadNumber == squadNumber);
        unitsInSquad.Foreach(x =>
        {
            x.RecursiveDestroy();
        });
    }
}