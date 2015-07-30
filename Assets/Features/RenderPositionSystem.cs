using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderPositionSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View); } }

    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var pos = e.position.Position;
            e.view.GameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }
}
