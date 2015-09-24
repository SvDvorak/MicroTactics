using System.Collections.Generic;
using Entitas;
using Object = UnityEngine.Object;

public class RemoveViewSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Destroy, Matcher.View).OnEntityAdded(); } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var view = entity.view.Value;
            GameObjectConfigurer.DetachEntity(view, entity);

            if (!entity.isLeavingBody)
            {
                Object.Destroy(view);
            }
        }
    }
}
