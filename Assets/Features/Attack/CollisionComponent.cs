using Entitas;
using UnityEngine;
using Vector3 = Mono.GameMath.Vector3;

public class CollisionComponent : IComponent
{
    public Entity Entity;
    public GameObject View;
    public Vector3 RelativeVelocity;
}