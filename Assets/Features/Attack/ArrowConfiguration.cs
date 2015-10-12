using Entitas;
using UnityEngine;

public class ArrowConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public GameObject PhysicsObject;
    private Collidable _collidable;

    public void OnAttachEntity(Entity entity)
    {
        entity.AddPhysics(PhysicsObject.GetComponent<Rigidbody>());
        _collidable = PhysicsObject.AddComponent<Collidable>();
        _collidable.SetEntity(entity);
    }

    public void OnDetachEntity(Entity entity)
    {
        Destroy(_collidable);
    }
}
