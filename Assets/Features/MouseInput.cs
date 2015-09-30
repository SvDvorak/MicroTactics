using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets;
using Entitas;

public class MouseInput : MonoBehaviour
{
    private Entity _entity;
    private Group _viewableGroup;

    private Entity InputEntity
    {
        get { return _entity ?? (_entity = Pools.pool.CreateEntity()); }
    }

    void Start()
    {
        _viewableGroup = Pools.pool.GetGroup(Matcher.View);
    }

    void Update ()
	{
        var input = WilInput.Instance;
        var possibleHits = input.RaycastFromMousePosition();
        var hitEntities = GetMatchingEntitiesForHits(possibleHits).ToList();

        if (!hitEntities.Any())
        {
            return;
        }

        if (input.GetMouseButtonDown(0))
        {
            InputEntity.ReplaceInput(InputState.Press, hitEntities);
        }
        else if (input.GetMouseButtonUp(0))
        {
            InputEntity.ReplaceInput(InputState.Release, hitEntities);
        }
        else
        {
            InputEntity.ReplaceInput(InputState.Hover, hitEntities);
        }
    }

    private List<EntityHit> GetMatchingEntitiesForHits(IEnumerable<RayHit> possibleHits)
    {
        var viewables = _viewableGroup.GetEntities();
        return possibleHits
            .Select(hit => new EntityHit(GetMatchingEntity(viewables, hit), hit.Point.ToV3()))
            .Where(hit => hit.Entity != null)
            .ToList();
    }

    private static Entity GetMatchingEntity(Entity[] viewables, RayHit hit)
    {
        return viewables.SingleOrDefault(x => x.view.Value == hit.Transform.gameObject);
    }
}
 