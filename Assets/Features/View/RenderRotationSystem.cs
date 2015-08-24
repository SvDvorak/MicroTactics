using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;

public class RenderRotationSystem : IReactiveSystem, IEnsureComponents
{
    public IMatcher trigger { get { return Matcher.Rotation; } }
    public IMatcher ensureComponents { get { return Matcher.View; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var transform = e.view.GameObject.transform;
            transform.rotation = e.rotation.ToUnityQ();
        }
    }
}