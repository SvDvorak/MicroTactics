using Entitas;
using UnityEngine;

public class ArrowConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public Rigidbody PhysicsObject;

    public void OnAttachEntity(Entity entity)
    {
        entity.AddPhysics(PhysicsObject);
    }

    public void OnDetachEntity(Entity entity)
    {
        entity.RemovePhysics();
    }
}
