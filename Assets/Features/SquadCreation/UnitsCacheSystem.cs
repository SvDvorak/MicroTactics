using System.Collections.Generic;
using System.Linq;
using Entitas;

public class UnitsCacheSystem : IMultiReactiveSystem, ISetPool
{
    private Group _squads;

    public TriggerOnEvent[] triggers
    {
        get
        {
            return new[]
                {
                    Matcher.Unit.OnEntityAddedOrRemoved(),
                    Matcher.AllOf(Matcher.Unit, Matcher.Destroy).OnEntityAdded()
                };
        }
    }

    public void SetPool(Pool pool)
    {
        _squads = pool.GetGroup(Matcher.Squad);
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var squadEntity in _squads.GetEntities())
        {
            var unitsToRemove = entities.Where(x => !x.hasUnit || x.isDestroy);
            var unitsToAdd = entities.Where(x => x.hasUnit && x.unit.SquadNumber == squadEntity.squad.Number);

            var existingUnits = new List<Entity>();
            if (squadEntity.hasUnitsCache)
            {
                existingUnits = squadEntity.unitsCache.Units;
            }

            var updatedUnits = existingUnits.Union(unitsToAdd).Except(unitsToRemove).ToList();
            squadEntity.ReplaceUnitsCache(updatedUnits);
        }
    }
}