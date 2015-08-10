using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RemovePhysicsSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.Physics; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityRemoved; } }

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