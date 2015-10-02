using Assets;
using Entitas;
using UnityEngine;

public class ApplyPhysicsSystem : IExecuteSystem, ISetPool
{
    private Group _leftBodiesGroup;
    private Group _forceGroup;

    public void SetPool(Pool pool)
    {
        _leftBodiesGroup = pool.GetGroup(Matcher.AllOf(Matcher.LeavingBody, Matcher.Physics));
        _forceGroup = pool.GetGroup(Matcher.AllOf(Matcher.Force, Matcher.Physics));
    }

    public void Execute()
    {
        foreach (var entity in _leftBodiesGroup.GetEntities())
        {
            entity.physics.RigidBody.constraints = RigidbodyConstraints.None;
            entity.physics.RigidBody.useGravity = true;
        }

        foreach (var entity in _forceGroup.GetEntities())
        {
            entity.physics.RigidBody.AddForce(entity.force.ToUnityV3());
            entity.RemoveForce();
        }
    }
}