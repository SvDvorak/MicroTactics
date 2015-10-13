using System;
using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public float MovementSpeed;
    public Rigidbody AttachRoot;
    private Collidable _collidable;

    public void OnAttachEntity(Entity entity)
    {
        entity
            .AddPhysics(AttachRoot)
            .AddAttachRoot(AttachRoot.gameObject)
            .AddMovement(MovementSpeed);

        _collidable = gameObject.AddComponent<Collidable>();
        _collidable.SetEntity(entity);
    }

    public void OnDetachEntity(Entity entity)
    {
        Destroy(gameObject.GetComponent<Collidable>());
    }
}