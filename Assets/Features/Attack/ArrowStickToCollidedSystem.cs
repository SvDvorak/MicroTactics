using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using UnityEngine;

public class ArrowStickToCollidedSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Collision.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.Stickable, Matcher.Physics); } }

    public void Execute(List<Entity> entities)
    {
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
        return entity.collision.RelativeVelocity.Length() > entity.stickable.RequiredVelocity;
    }
}