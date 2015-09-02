using System.Collections.Generic;
using Assets;
using Entitas;

public class RenderPositionSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View).OnEntityAdded(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var transform = e.view.GameObject.transform;
            transform.position = e.position.ToUnityV3();
        }
    }
}
