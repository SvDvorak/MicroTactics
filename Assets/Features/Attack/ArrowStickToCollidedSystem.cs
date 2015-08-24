using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ArrowStickToCollidedSystem : IReactiveSystem, IEnsureComponents
{
    public IMatcher trigger { get { return Matcher.Collision; } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.Arrow, Matcher.Physics); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        var enumerable = entities.Where(x => x.collision.CollidedWith != null && x.collision.CollidedWith.hasUnit);
        if (enumerable.Any())
        {
            int i = 0;
        }
        foreach (var entity in entities.Where(ShouldStick))
        {
            entity.RemovePhysics();
            if (entity.collision.CollidedWith != null)
            {
                entity.collision.CollidedWith.AddChildTwoWay(entity);
            }
        }
    }

    private static bool ShouldStick(Entity entity)
    {
        return entity.collision.RelativeVelocity.Length() > 10;
    }
}