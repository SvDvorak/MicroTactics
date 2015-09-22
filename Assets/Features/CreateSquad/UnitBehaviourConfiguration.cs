using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IEntityConfigurer
{
    public float MovementSpeed;
    public GameObject AttachRoot;

    public void OnConfigureEntity(Entity entity)
    {
        entity
            .AddAttachRoot(AttachRoot)
            .AddMovement(MovementSpeed);

        gameObject.AddComponent<Collidable>().SetEntity(entity);
    }
}