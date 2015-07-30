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

    public IMatcher trigger { get { return Matcher.Squad; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
        _unitsGroup = pool.GetGroup(Matcher.Unit);
    }

    public void Execute(List<Entity> entities)
    {
        var squad = entities.SingleEntity().squad;

        RemoveExistingUnitsFromSquad(squad.Number);
        CreateUnitsForSquad(squad);
    }

    private void CreateUnitsForSquad(SquadComponent squad)
    {
        for (int row = 0; row < squad.Rows; row++)
        {
            for (int column = 0; column < squad.Columns; column++)
            {
                _pool.CreateEntity()
                    .AddUnit(squad.Number)
                    .AddPosition(column, 0, row);
            }
        }
    }

    private void RemoveExistingUnitsFromSquad(int squadNumber)
    {
        var unitsInSquad = _unitsGroup.GetEntities().Where(x => x.unit.SquadNumber == squadNumber);
        unitsInSquad.Foreach(x => x.IsDestroy(true));
    }
}