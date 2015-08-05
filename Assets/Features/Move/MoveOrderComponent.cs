using Entitas;
using Mono.GameMath;

public class MoveOrderComponent : VectorClass, IComponent
{
}

public static class EntityMoveOrderExtensions
{
    public static Entity AddMoveOrder(this Entity entity, Vector3 vector)
    {
        return entity.AddMoveOrder(vector.X, vector.Y, vector.Z);
    }

    public static Entity ReplaceMoveOrder(this Entity entity, Vector3 vector)
    {
        return entity.ReplaceMoveOrder(vector.X, vector.Y, vector.Z);
    }
}