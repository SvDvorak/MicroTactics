using System;
using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public float MovementSpeed;
    public GameObject AttachRoot;
    private Collidable _collidable;

    public void OnAttachEntity(Entity entity)
    {
        entity
            .AddAttachRoot(AttachRoot)
            .AddMovement(MovementSpeed);

        _collidable = gameObject.AddComponent<Collidable>();
        _collidable.SetEntity(entity);
    }

    public void OnDetachEntity(Entity entity)
    {
        Destroy(gameObject.GetComponent<Collidable>());
    }
}