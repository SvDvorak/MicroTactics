using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RemovePhysicsSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Physics.OnEntityRemoved(); } }
    public IMatcher ensureComponents { get { return Matcher.View; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            Object.Destroy(entity.view.Value.GetComponent<Rigidbody>());
        }
    }
}