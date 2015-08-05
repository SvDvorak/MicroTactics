using Entitas;
using Mono.GameMath;

public class PositionComponent : VectorClass, IComponent
{
}

public static class EntityPositionExtensions
{
    public static Entity AddPosition(this Entity entity, Vector3 vector)
    {
        return entity.AddPosition(vector.X, vector.Y, vector.Z);
    }

    public static Entity ReplacePosition(this Entity entity, Vector3 vector)
    {
        return entity.ReplacePosition(vector.X, vector.Y, vector.Z);
    }
}