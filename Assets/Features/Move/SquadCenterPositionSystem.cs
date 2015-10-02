using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using Mono.GameMath;

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
            unitCacheEntity.ReplacePosition(centerPosition);
        }
    }

    private Vector3 GetCenterPositionFromUnits(Entity unitCacheEntity)
    {
        return unitCacheEntity
            .unitsCache.Units
            .Sum(x => x.position.ToV3())/unitCacheEntity.unitsCache.Units.Count();
    }
}