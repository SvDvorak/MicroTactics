using System.Collections.Generic;
using System.Linq;
using Entitas;

public class UnitsCacheSystem : IReactiveSystem, ISetPool
{
    private Group _squads;

    public IMatcher trigger { get { return Matcher.Unit; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAddedOrRemoved; } }

    public void SetPool(Pool pool)
    {
        _squads = pool.GetGroup(Matcher.Squad);
    }

    public void Execute(List<Entity> entities)
    {
        var unitsToRemove = entities.Where(x => !x.hasUnit || x.isDestroy);
        var unitsToAdd = entities.Where(x => x.hasUnit);
        var squad = _squads.GetSingleEntity();

        var existingUnits = new List<Entity>();
        if (squad.hasUnitsCache)
        {
            existingUnits = squad.unitsCache.Units;
        }

        var updatedUnits = existingUnits.Union(unitsToAdd).Except(unitsToRemove).ToList();
        squad.ReplaceUnitsCache(updatedUnits);
    }
}