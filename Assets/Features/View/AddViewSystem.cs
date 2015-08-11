using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddViewSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.Resource; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var resourceObject = Resources.Load<GameObject>(entity.resource.Name);
            entity.AddView(Object.Instantiate(resourceObject));
        }
    }
}