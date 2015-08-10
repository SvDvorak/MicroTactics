using System.Collections.Generic;
using Entitas;

public class ArrowStickToCollidedSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Arrow, Matcher.Physics, Matcher.Collision); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.RemovePhysics();
        }
    }
}