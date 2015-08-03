using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

public class SquadMoveOrderSystem : IReactiveSystem, ISetPool
{
    private Group _positionableUnits;

    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Squad, Matcher.MoveOrder); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _positionableUnits = pool.GetGroup(Matcher.AllOf(Matcher.Unit, Matcher.Position));
    }

    public void Execute(List<Entity> entities)
    {
        var squadUnitGroups = _positionableUnits.GetEntities().Where(x => !x.isDestroy).GroupBy(x => x.unit.SquadNumber).ToList();

        foreach (var squadEntity in entities)
        {
            var unitsInSquad = squadUnitGroups.Find(x => x.Key == squadEntity.squad.Number);
            if (squadEntity.boxFormation.Columns * squadEntity.boxFormation.Rows == 0 || unitsInSquad == null)
            {
                continue;
            }

            OrderUnitsToSquadRelativePositions(unitsInSquad.ToList(), squadEntity);
        }
    }

    private static void OrderUnitsToSquadRelativePositions(List<Entity> unitsInSquad, Entity squadEntity)
    {
        for (var i = 0; i < unitsInSquad.Count(); i++)
        {
            var unit = unitsInSquad.ElementAt(i);
            var squadPosition = UnitInSquadPositioner.GetPosition(squadEntity.boxFormation, i);

            if (unit.hasMoveOrder)
            {
                unit.RemoveMoveOrder();
            }

            unit.AddMoveOrder(
                squadPosition.x + squadEntity.moveOrder.x,
                squadPosition.y + squadEntity.moveOrder.y,
                squadPosition.z + squadEntity.moveOrder.z);
        }
    }
}
