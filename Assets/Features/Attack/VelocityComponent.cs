using Entitas;
using Mono.GameMath;

public class VelocityComponent : VectorClass, IComponent
{
}

public static class EntityVelocityExtensions
{
    public static Entity AddVelocity(this Entity entity, Vector3 vector)
    {
        return entity.AddVelocity(vector.X, vector.Y, vector.Z);
    }

    public static Entity ReplaceVelocity(this Entity entity, Vector3 vector)
    {
        return entity.ReplaceVelocity(vector.X, vector.Y, vector.Z);
    }
}