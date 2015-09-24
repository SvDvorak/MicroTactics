using Entitas;
using UnityEngine;

public class PhysicsComponent : IComponent
{
    public Rigidbody RigidBody;
}

public class LimitPhysicsComponent : IComponent
{
    public LimitedAxes Position;
    public LimitedAxes Rotation;
}

public struct LimitedAxes
{
    public LimitedAxes(bool x, bool y, bool z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public bool X;
    public bool Y;
    public bool Z;

    public static LimitedAxes None { get { return new LimitedAxes(false, false, false); } }
}