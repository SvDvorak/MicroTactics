using Entitas;
using UnityEngine;

public class UnitBehaviourConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public float MovementSpeed;
    public GameObject AttachRoot;

    public void OnAttachEntity(Entity entity)
    {
        var limitYAxis = new LimitedAxes(false, true, false);
        var limitAllAxes = new LimitedAxes(true, true, true);

        entity
            .AddAttachRoot(AttachRoot)
            .AddMovement(MovementSpeed)
            .AddLimitPhysics(limitYAxis, limitAllAxes);

        gameObject.AddComponent<Collidable>().SetEntity(entity);
    }

    public void OnDetachEntity(Entity entity)
    {
        Destroy(gameObject.GetComponent<Collidable>());
    }
}