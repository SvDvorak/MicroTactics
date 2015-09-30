using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class HideHiddenSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Hidden.OnEntityAddedOrRemoved(); } }
    public IMatcher ensureComponents { get { return Matcher.View; } }


    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.view.Value.GetComponent<MeshRenderer>().enabled = !entity.isHidden;
        }
    }
}