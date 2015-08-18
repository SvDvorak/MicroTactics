using System.Collections.Generic;
using Assets;
using Entitas;

public class RenderPositionSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var transform = e.view.GameObject.transform;
            transform.position = e.position.ToUnityV3();
        }
    }
}
