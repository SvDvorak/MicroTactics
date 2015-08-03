using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class SquadCenterPositionSystem : IExecuteSystem, ISetPool
{
    private Group _squads;
    private Group _units;

    public void SetPool(Pool pool)
    {
        _squads = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.Position));
        _units = pool.GetGroup(Matcher.AllOf(Matcher.Unit, Matcher.Position));
    }

    public void Execute()
    {
        foreach (var squadEntity in _squads.GetEntities())
        {
            var centerPosition = GetCenterPositionFromUnits(squadEntity);
            squadEntity.ReplacePosition(centerPosition.x, centerPosition.y, centerPosition.z);
        }
    }

    private Vector3 GetCenterPositionFromUnits(Entity squadEntity)
    {
        var unitsInSquad = _units.GetEntities()
            .Where(x => x.unit.SquadNumber == squadEntity.squad.Number)
            .ToList();

        var centerPosition = unitsInSquad.Sum(x => x.position.ToV3())/unitsInSquad.Count();
        return centerPosition;
    }
}