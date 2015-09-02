using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using Mono.GameMath;

public class SquadMoveOrderSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation, Matcher.MoveOrder).OnEntityAdded(); } }


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

            unit.ReplaceMoveOrder(squadPosition + squadEntity.moveOrder.Position, Quaternion.Identity);
        }
    }
}