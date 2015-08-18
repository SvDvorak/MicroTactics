using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;
using Vexe.Runtime.Extensions;

public class SquadCreationSystem : IReactiveSystem, ISetPool
{
    private Group _unitsGroup;
    private Pool _pool;
    private Group _selectionAreasGroup;

    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Squad, Matcher.BoxFormation); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
        _unitsGroup = pool.GetGroup(Matcher.Unit);
        _selectionAreasGroup = pool.GetGroup(Matcher.SelectionArea);
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var squadEntity in entities)
        {
            RemoveExistingUnitsFromSquad(squadEntity.squad.Number);
            RemoveExistingSelectionArea(squadEntity);
            CreateUnitsForSquad(squadEntity.squad, squadEntity.boxFormation);
            CreateSelectionArea(squadEntity);
        }
    }

    private void CreateUnitsForSquad(SquadComponent squad, BoxFormationComponent formation)
    {
        for (var i = 0; i < formation.Columns*formation.Rows; i++)
        {
            var position = UnitInSquadPositioner.GetPosition(formation, i);
            _pool.CreateEntity()
                 .AddUnit(squad.Number)
                 .AddResource(Res.Unit)
                 .AddPosition(position)
                 .AddRotation(Quaternion.Identity)
                 .AddMovement(0.06f);
        }
    }

    private void CreateSelectionArea(Entity squadEntity)
    {
        _pool
            .CreateEntity()
            .AddSelectionArea(squadEntity)
            .AddResource(Res.SelectionArea);
    }

    private void RemoveExistingUnitsFromSquad(int squadNumber)
    {
        var unitsInSquad = _unitsGroup.GetEntities().Where(x => x.unit.SquadNumber == squadNumber);
        unitsInSquad.Foreach(x => x.IsDestroy(true));
    }

    private void RemoveExistingSelectionArea(Entity squadEntity)
    {
        _selectionAreasGroup
            .GetEntities()
            .Where(x => x.selectionArea.Parent == squadEntity)
            .Foreach(x => x.IsDestroy(true));
    }
}