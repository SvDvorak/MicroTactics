using System.Collections.Generic;
using Assets;
using Entitas;

public class ApplyForceSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Force, Matcher.Physics); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.physics.RigidBody.AddForce(entity.force.ToUnityV3());
        }
    }
}