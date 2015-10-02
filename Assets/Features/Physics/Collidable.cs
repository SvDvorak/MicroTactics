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
        var otherCollidable = collision.gameObject.GetComponent<Collidable>();
        if (otherCollidable == null || Entity == null || Entity.hasCollision)
        {
            return;
        }

        Entity.AddCollision(otherCollidable.Entity, collision.relativeVelocity.ToV3());
    }
}