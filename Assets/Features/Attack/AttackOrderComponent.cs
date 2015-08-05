using Entitas;
using Mono.GameMath;

public class AttackOrderComponent : VectorClass, IComponent
{
}

public static class EntityAttackOrderExtensions
{
    public static Entity AddAttackOrder(this Entity entity, Vector3 vector)
    {
        return entity.AddAttackOrder(vector.X, vector.Y, vector.Z);
    }

    public static Entity ReplaceAttackOrder(this Entity entity, Vector3 vector)
    {
        return entity.ReplaceAttackOrder(vector.X, vector.Y, vector.Z);
    }
}