using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public float MovementSpeed;
    public GameObject AttachRoot;

    public void OnAttachEntity(Entity entity)
    {
        entity
            .AddAttachRoot(AttachRoot)
            .AddMovement(MovementSpeed);

        gameObject.AddComponent<Collidable>().SetEntity(entity);
    }

    public void OnDetachEntity(Entity entity)
    {
        Destroy(gameObject.GetComponent<Collidable>());
    }
}