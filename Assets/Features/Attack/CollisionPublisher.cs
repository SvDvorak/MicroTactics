﻿using Entitas;
using UnityEngine;

public class CollisionPublisher : MonoBehaviour
{
    public Entity Entity { get; private set; }

    public void SetEntity(Entity entity)
    {
        Entity = entity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        var otherPublisher = collision.collider.GetComponent<CollisionPublisher>();
        if (otherPublisher == null || Entity == null || Entity.hasCollision)
        {
            return;
        }

        Entity.AddCollision(otherPublisher.Entity);
    }
}