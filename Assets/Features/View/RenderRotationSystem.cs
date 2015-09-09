using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;

public class RenderRotationSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Rotation.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.View; } }


    public void Execute(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            var transform = e.view.Value.transform;
            transform.rotation = e.rotation.ToUnityQ();
        }
    }
}