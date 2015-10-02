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
        var mouseState = input.GetMouseState(0);
        var hitEntities = GetMatchingEntitiesForHits(mouseState.Hits).ToList();

        if (!hitEntities.Any())
        {
            return;
        }

        InputEntity.ReplaceInput(mouseState.State, hitEntities);
    }

    private List<EntityHit> GetMatchingEntitiesForHits(IEnumerable<RayHit> hits)
    {
        var viewables = _viewableGroup.GetEntities();
        return hits
            .Select(hit => new EntityHit(GetMatchingEntity(viewables, hit), hit.Point.ToV3()))
            .Where(hit => hit.Entity != null)
            .ToList();
    }

    private static Entity GetMatchingEntity(Entity[] viewables, RayHit hit)
    {
        return viewables.SingleOrDefault(x => x.view.Value == hit.Transform.gameObject);
    }
}
 