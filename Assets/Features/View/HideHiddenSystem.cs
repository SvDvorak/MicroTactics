using System.Collections.Generic;
using Entitas;

public class HideHiddenSystem : IReactiveSystem, IEnsureComponents
{
    public IMatcher trigger { get { return Matcher.Hidden; } }
    public IMatcher ensureComponents { get { return Matcher.View; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAddedOrRemoved; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.view.GameObject.SetActive(!entity.isHidden);
        }
    }
}