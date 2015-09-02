using System.Linq;
using Assets;
using Entitas;
using UnityEngine;

public class LinkViewsStartSystem : IInitializeSystem, ISetPool
{
    private Pool _pool;

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Initialize()
    {
        var componentContainers = Object.FindObjectsOfType<ComponentContainer>();
        foreach (var componentContainer in componentContainers)
        {
            CreateEntityFor(componentContainer);
        }
    }

    private void CreateEntityFor(ComponentContainer componentContainer)
    {
        var entity = _pool.CreateEntity();
        foreach (var component in componentContainer.Components)
        {
            entity.AddComponent(ComponentIds.ComponentToId(component), component);

            if (component is PositionComponent)
            {
                var objectPosition = componentContainer.transform.position;
                entity.ReplacePosition(objectPosition.ToV3());
            }
        }
    }
}