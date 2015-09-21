using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ArrowStickToCollidedSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Collision.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.Stickable, Matcher.Physics); } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities.Where(ShouldStick))
        {
            entity.RemovePhysics();

            var otherEntity = entity.collision.OtherEntity;
            if (otherEntity != null)
            {
                entity.AddAttachTo(entity.collision.OtherEntity);
            }
        }
    }

    private static bool ShouldStick(Entity entity)
    {
        return entity.collision.RelativeVelocity.Length() > entity.stickable.RequiredVelocity;
    }
}