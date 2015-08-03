using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class AddUnitViewSystem : IReactiveSystem, ISetPool
{
    private Group _squads;

    private readonly Transform _unitContainer = new GameObject("Units").transform;

    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Unit, Matcher.Position); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _squads = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.UnitTemplate));
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
        var squad = GetMatchingSquad(entity);
        var gameObject = InstantiateGameObject(entity, squad.unitTemplate.Template);
        var animator = gameObject.GetComponent<Animator>();

        entity
            .AddView(gameObject)
            .AddAnimate(animator);
    }

    private Entity GetMatchingSquad(Entity entity)
    {
        return _squads.GetEntities().Single(x => x.squad.Number == entity.unit.SquadNumber);
    }

    private GameObject InstantiateGameObject(Entity entity, GameObject unitTemplate)
    {
        var gameObject =
            (GameObject) Object.Instantiate(unitTemplate, entity.position.ToV3(), new Quaternion());
        gameObject.transform.SetParent(_unitContainer);

        return gameObject;
    }
}