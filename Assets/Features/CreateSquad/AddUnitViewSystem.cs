using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class AddUnitViewSystem : IReactiveSystem, ISetPool
{
    private Group _squads;

    private readonly Transform _unitContainer = new GameObject("Units").transform;

    public IMatcher trigger { get { return Matcher.Unit; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _squads = pool.GetGroup(Matcher.Squad);
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (!entity.hasView)
            {
                AddUnitViewUsingSquadTemplate(entity);
            }
        }
    }

    private void AddUnitViewUsingSquadTemplate(Entity entity)
    {
        var squad = _squads.GetEntities().Single(x => x.squad.Number == entity.unit.SquadNumber);
        var gameObject = Object.Instantiate(squad.unitTemplate.Template);
        gameObject.transform.SetParent(_unitContainer);
        entity.AddView(gameObject);
    }
}