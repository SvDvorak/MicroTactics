using System.Collections.Generic;
using Entitas;

public class ShowSelectedIndicatorForSquadSystem : IReactiveSystem, IEnsureComponents
{
    public IMatcher trigger { get { return Matcher.Selected; } }
    public IMatcher ensureComponents { get { return Matcher.UnitsCache; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAddedOrRemoved; } }

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
            foreach (var child in unit.child.Value)
            {
                child.IsHidden(!unitsCacheEntity.isSelected);
            }
        }
    }
}