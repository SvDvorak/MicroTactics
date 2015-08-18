using System;
using Entitas;
using Mono.GameMath;

public class RotationComponent : QuaternionClass, IComponent
{
}

public static class EntityRotationExtensions
{
    public static Entity AddRotation(this Entity entity, Quaternion quaternion)
    {
        return entity.AddRotation(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }

    public static Entity ReplaceRotation(this Entity entity, Quaternion quaternion)
    {
        return entity.ReplaceRotation(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }
}