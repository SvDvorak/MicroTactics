using System.Collections.Generic;
using Assets;
using Entitas;

public class ApplyForceSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Force, Matcher.Physics).OnEntityAdded(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.physics.RigidBody.AddForce(entity.force.ToUnityV3());
        }
    }
}