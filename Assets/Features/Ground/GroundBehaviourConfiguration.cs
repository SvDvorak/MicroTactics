using Entitas;
using UnityEngine;

public class GroundBehaviourConfiguration : MonoBehaviour, IGameObjectConfigurer
{
    public void OnAttachEntity(Entity entity)
    {
        entity.AddView(gameObject);
    }

    public void OnDetachEntity(Entity entity)
    {
    }
}