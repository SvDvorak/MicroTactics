using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IEntityConfigurer
{
    public float MovementSpeed;
    public Collider Collider;

    public void OnConfigureEntity(Entity entity)
    {
        entity
            .AddMovement(MovementSpeed);

        gameObject.AddComponent<Collidable>().SetEntity(entity);

        //var collidableEntity = Pools.pool.CreateEntity().AddView(Collidable.gameObject);
        //Collidable.SetEntity(collidableEntity);
    }
}