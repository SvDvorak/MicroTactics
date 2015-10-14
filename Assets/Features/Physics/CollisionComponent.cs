using Entitas;
using UnityEngine;
using Vector3 = Mono.GameMath.Vector3;

public class CollisionComponent : IComponent
{
    public Collider Collider;
    public Vector3 RelativeVelocity;
}