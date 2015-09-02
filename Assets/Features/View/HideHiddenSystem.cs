using System.Collections.Generic;
using Entitas;

public class HideHiddenSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Hidden.OnEntityAddedOrRemoved(); } }
    public IMatcher ensureComponents { get { return Matcher.View; } }


    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.view.GameObject.SetActive(!entity.isHidden);
        }
    }
}