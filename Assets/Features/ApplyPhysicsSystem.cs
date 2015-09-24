using Assets;
using Entitas;
using UnityEngine;

public class ApplyPhysicsSystem : IExecuteSystem, ISetPool
{
    private Group _leftBodies;
    private Group _limitedPhysics;
    private Group _forceGroup;

    public void SetPool(Pool pool)
    {
        _leftBodies = pool.GetGroup(Matcher.AllOf(Matcher.LeavingBody, Matcher.Physics));
        _limitedPhysics = pool.GetGroup(Matcher.AllOf(Matcher.LimitPhysics, Matcher.Physics));
        _forceGroup = pool.GetGroup(Matcher.AllOf(Matcher.Force, Matcher.Physics));
    }

    public void Execute()
    {
        foreach (var entity in _leftBodies.GetEntities())
        {
            entity.ReplaceLimitPhysics(LimitedAxes.None, LimitedAxes.None);
            entity.physics.RigidBody.useGravity = true;
        }

        foreach (var entity in _limitedPhysics.GetEntities())
        {
            entity.physics.RigidBody.constraints = ToConstraints(entity.limitPhysics);
        }

        foreach (var entity in _forceGroup.GetEntities())
        {
            entity.physics.RigidBody.AddForce(entity.force.ToUnityV3());
            entity.RemoveForce();
        }
    }

    private RigidbodyConstraints ToConstraints(LimitPhysicsComponent limitPhysics)
    {
        RigidbodyConstraints constraints = RigidbodyConstraints.None;
        if (limitPhysics.Position.X) { constraints = constraints | RigidbodyConstraints.FreezePositionX; }
        if (limitPhysics.Position.Y) { constraints = constraints | RigidbodyConstraints.FreezePositionY; }
        if (limitPhysics.Position.Z) { constraints = constraints | RigidbodyConstraints.FreezePositionZ; }
        if (limitPhysics.Rotation.X) { constraints = constraints | RigidbodyConstraints.FreezeRotationX; }
        if (limitPhysics.Rotation.Y) { constraints = constraints | RigidbodyConstraints.FreezeRotationY; }
        if (limitPhysics.Rotation.Z) { constraints = constraints | RigidbodyConstraints.FreezeRotationZ; }

        return constraints;
    }
}