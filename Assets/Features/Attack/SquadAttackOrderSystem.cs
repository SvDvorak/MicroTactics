using System.Collections.Generic;
using Entitas;

public class SquadAttackOrderSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AttackOrder; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var squadEntity in entities)
        {
            if (!squadEntity.hasUnitsCache || !squadEntity.hasBoxFormation)
            {
                continue;
            }

            SetAttackRelativeToSquadPosition(squadEntity.unitsCache.Units, squadEntity);
        }
    }

    private static void SetAttackRelativeToSquadPosition(IList<Entity> unitsInSquad, Entity squadEntity)
    {
        for (var i = 0; i < unitsInSquad.Count; i++)
        {
            var unit = unitsInSquad[i];
            var squadPosition = UnitInSquadPositioner.GetPosition(squadEntity.boxFormation, i);

            // TODO: Remove!
            if (unit.hasAttackOrder)
            {
                continue;
            }

            unit.ReplaceAttackOrder(
                squadPosition.X + squadEntity.attackOrder.x,
                squadPosition.Y + squadEntity.attackOrder.y,
                squadPosition.Z + squadEntity.attackOrder.z);
        }
    }
}