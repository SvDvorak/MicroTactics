using System.Collections.Generic;
using Entitas;

public class ShowSelectedIndicatorForSquadSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Selected.OnEntityAddedOrRemoved(); } }
    public IMatcher ensureComponents { get { return Matcher.UnitsCache; } }


    public void Execute(List<Entity> entities)
    {
        foreach (var unitsCacheEntity in entities)
        {
            SetHiddenForUnitsCache(unitsCacheEntity);
        }
    }

    private static void SetHiddenForUnitsCache(Entity unitsCacheEntity)
    {
        foreach (var unit in unitsCacheEntity.unitsCache.Units)
        {
            foreach (var child in unit.children.Value)
            {
                child.IsHidden(!unitsCacheEntity.isSelected);
            }
        }
    }
}