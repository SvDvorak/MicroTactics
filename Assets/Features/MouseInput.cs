using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

        if (!possibleHits.Any())
        {
            return;
        }

        var hitEntities = GetMatchingEntitiesForHits(possibleHits);

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

    private List<Entity> GetMatchingEntitiesForHits(RaycastHit[] possibleHits)
    {
        var viewables = _viewableGroup.GetEntities();
        return possibleHits
            .Select(hit => viewables.SingleOrDefault(x => x.view.GameObject == hit.transform.gameObject))
            .Where(hitEntity => hitEntity != null)
            .ToList();
    }

    private RaycastHit[] RaycastUsingCamera()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastAll(ray, Mathf.Infinity);
    }
}
 