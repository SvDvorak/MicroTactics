using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RemovePhysicsSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.Physics.OnEntityRemoved(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.hasView)
            {
                Object.Destroy(entity.view.GameObject.GetComponent<Rigidbody>());
            }
        }
    }
}