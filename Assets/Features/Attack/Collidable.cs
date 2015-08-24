using System.Linq;
using Assets;
using Entitas;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public Entity Entity { get; private set; }

    public void SetEntity(Entity entity)
    {
        Entity = entity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        var otherPublisher = collision.collider.GetComponent<Collidable>();
        if (otherPublisher == null || Entity == null || Entity.hasCollision)
        {
            return;
        }

        Entity.AddCollision(otherPublisher.Entity, collision.relativeVelocity.ToV3());
    }
}