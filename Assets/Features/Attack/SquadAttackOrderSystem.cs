using System.Collections.Generic;
using Entitas;

public class SquadAttackOrderSystem : IReactiveSystem, IEnsureComponents
{
    public IMatcher trigger { get { return Matcher.AttackOrder; } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var squadEntity in entities)
        {
            SetAttackRelativeToSquadPosition(squadEntity.unitsCache.Units, squadEntity);
        }
    }

    private static void SetAttackRelativeToSquadPosition(IList<Entity> unitsInSquad, Entity squadEntity)
    {
        for (var i = 0; i < unitsInSquad.Count; i++)
        {
            var unit = unitsInSquad[i];
            var squadPosition = UnitInSquadPositioner.GetPosition(squadEntity.boxFormation, i);

            unit.ReplaceAttackOrder(
                squadPosition.X + squadEntity.attackOrder.x,
                squadPosition.Y + squadEntity.attackOrder.y,
                squadPosition.Z + squadEntity.attackOrder.z);
        }
    }
}