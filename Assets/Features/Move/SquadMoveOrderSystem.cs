using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;

public class SquadMoveOrderSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation, Matcher.MoveOrder); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var squadEntity in entities)
        {
            OrderUnitsToSquadRelativePositions(squadEntity.unitsCache.Units, squadEntity);
        }
    }

    private static void OrderUnitsToSquadRelativePositions(IList<Entity> unitsInSquad, Entity squadEntity)
    {
        for (var i = 0; i < unitsInSquad.Count; i++)
        {
            var unit = unitsInSquad[i];
            var squadPosition = UnitInSquadPositioner.GetPosition(squadEntity.boxFormation, i);

            unit.ReplaceMoveOrder(squadPosition + squadEntity.moveOrder.ToV3());
        }
    }
}