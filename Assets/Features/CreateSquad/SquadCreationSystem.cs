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
        for (int y = 0; y < squad.Rows; y++)
        {
            for (int x = 0; x < squad.Columns; x++)
            {
                _pool.CreateEntity()
                    .AddUnit(squad.Number)
                    .AddPosition(x, 0, y);
            }
        }
    }

    private void RemoveExistingUnitsFromSquad(int squadNumber)
    {
        var unitsInSquad = _unitsGroup.GetEntities().Where(x => x.unit.SquadNumber == squadNumber);
        unitsInSquad.Foreach(x => _pool.DestroyEntity(x));
    }
}