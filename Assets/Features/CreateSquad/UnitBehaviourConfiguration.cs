using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IEntityConfigurer
{
    public float MovementSpeed;
    public Collidable Collidable;

    public void OnConfigureEntity(Entity entity)
    {
        entity.AddMovement(MovementSpeed);

        var collidableEntity = Pools.pool.CreateEntity().AddView(Collidable.gameObject);
        Collidable.SetEntity(collidableEntity);
    }
}