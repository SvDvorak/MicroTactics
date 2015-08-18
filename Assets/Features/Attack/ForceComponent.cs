using Entitas;
using Mono.GameMath;

public class ForceComponent : VectorClass, IComponent
{
}

public static class EntityForceExtensions
{
    public static Entity AddForce(this Entity entity, Vector3 vector)
    {
        return entity.ReplaceForce(vector.X, vector.Y, vector.Z);
    }

    public static Entity ReplaceForce(this Entity entity, Vector3 vector)
    {
        return entity.ReplaceForce(vector.X, vector.Y, vector.Z);
    }
}