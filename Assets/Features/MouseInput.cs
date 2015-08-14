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
        var possibleHits = RaycastUsingCamera();
        var hitEntities = GetMatchingEntitiesForHits(possibleHits).ToList();

        if (!hitEntities.Any())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            InputEntity.ReplaceInput(InputState.Press, hitEntities);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            InputEntity.ReplaceInput(InputState.Release, hitEntities);
        }
        else
        {
            InputEntity.ReplaceInput(InputState.Hover, hitEntities);
        }
    }

    private List<EntityHit> GetMatchingEntitiesForHits(RaycastHit[] possibleHits)
    {
        var viewables = _viewableGroup.GetEntities();
        return possibleHits
            .Select(hit => new EntityHit(GetMatchingEntity(viewables, hit), hit.point.ToV3()))
            .Where(hit => hit.Entity != null)
            .ToList();
    }

    private static Entity GetMatchingEntity(Entity[] viewables, RaycastHit hit)
    {
        return viewables.SingleOrDefault(x => x.view.GameObject == hit.transform.gameObject);
    }

    private RaycastHit[] RaycastUsingCamera()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastAll(ray, Mathf.Infinity);
    }
}
 