using Entitas;
using Mono.GameMath;

public class CollisionComponent : IComponent
{
    public Entity CollidedWith;
    public Vector3 RelativeVelocity;
}