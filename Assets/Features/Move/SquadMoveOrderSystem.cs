using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        var squadUnitGroups = _positionableUnits.GetEntities().GroupBy(x => x.unit.SquadNumber).ToList();

        foreach (var squad in entities)
        {
            var unitsInSquad = squadUnitGroups.Find(x => x.Key == squad.squad.Number);
            OrderUnitsToSquadRelativePositions(unitsInSquad, squad);
        }
    }

    private static void OrderUnitsToSquadRelativePositions(IGrouping<int, Entity> unitsInSquad, Entity squadEntity)
    {
        for (var i = 0; i < unitsInSquad.Count(); i++)
        {
            var unit = unitsInSquad.ElementAt(i);
            var squadPositionX = i % squadEntity.squad.Columns;
            var squadPositionZ = i / squadEntity.squad.Rows;

            if (unit.hasMoveOrder)
            {
                unit.RemoveMoveOrder();
            }

            unit.AddMoveOrder(
                squadPositionX + squadEntity.moveOrder.x,
                squadEntity.moveOrder.y,
                squadPositionZ + squadEntity.moveOrder.z);
        }
    }
}
