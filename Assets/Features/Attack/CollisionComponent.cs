using Entitas;
using Vector3 = Mono.GameMath.Vector3;

public class CollisionComponent : IComponent
{
    public Entity OtherEntity;
    public Vector3 RelativeVelocity;
}