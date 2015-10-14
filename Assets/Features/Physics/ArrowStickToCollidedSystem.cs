using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class ArrowStickToCollidedSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.Collision.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.View, Matcher.Stickable, Matcher.Physics); } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities.Where(ShouldStick))
        {
            var parent = entity.collision.Collider.transform;
            var arrow = entity.view.Value;
            arrow.transform.SetParent(parent, true);
            arrow.PerformForHierarchy(SetKinematic);
        }
    }

    private static void SetKinematic(GameObject gameObject)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.isKinematic = true;
        }
    }

    private static bool ShouldStick(Entity entity)
    {
        return entity.collision.RelativeVelocity.Length() > entity.stickable.RequiredVelocity;
    }
}