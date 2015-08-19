using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IEntityConfigurer
{
    public float MovementSpeed;

    public void OnConfigureEntity(Entity entity)
    {
        entity.AddMovement(MovementSpeed);
    }
}