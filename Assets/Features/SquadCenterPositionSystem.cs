using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class SquadCenterPositionSystem : IExecuteSystem, ISetPool
{
    private Group _squads;

    public void SetPool(Pool pool)
    {
        _squads = pool.GetGroup(Matcher.AllOf(Matcher.UnitsCache, Matcher.Position));
    }

    public void Execute()
    {
        foreach (var unitCacheEntity in _squads.GetEntities())
        {
            var centerPosition = GetCenterPositionFromUnits(unitCacheEntity);
            unitCacheEntity.ReplacePosition(centerPosition.x, centerPosition.y, centerPosition.z);
        }
    }

    private Vector3 GetCenterPositionFromUnits(Entity unitCacheEntity)
    {
        var unitsInSquad = unitCacheEntity.unitsCache.Units;
        return unitsInSquad.Sum(x => x.position.ToV3())/unitsInSquad.Count();
    }
}