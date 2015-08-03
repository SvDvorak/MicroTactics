using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Vexe.Runtime.Extensions;

public class SquadCreationSystem : IReactiveSystem, ISetPool
{
    private Group _unitsGroup;
    private Pool _pool;

    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Squad, Matcher.BoxFormation); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
        _unitsGroup = pool.GetGroup(Matcher.Unit);
    }

    public void Execute(List<Entity> entities)
    {
        var squadEntity = entities.SingleEntity();

        RemoveExistingUnitsFromSquad(squadEntity.squad.Number);
        CreateUnitsForSquad(squadEntity.squad, squadEntity.boxFormation);
    }

    private void CreateUnitsForSquad(SquadComponent squad, BoxFormationComponent formation)
    {
        for (var i = 0; i < formation.Columns*formation.Rows; i++)
        {
            var position = UnitInSquadPositioner.GetPosition(formation, i);
            _pool.CreateEntity()
                 .AddUnit(squad.Number)
                 .AddPosition(position.x, position.y, position.z);
        }
    }

    private void RemoveExistingUnitsFromSquad(int squadNumber)
    {
        var unitsInSquad = _unitsGroup.GetEntities().Where(x => x.unit.SquadNumber == squadNumber);
        unitsInSquad.Foreach(x => x.IsDestroy(true));
    }
}