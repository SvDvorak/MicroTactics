using System.Collections.Generic;
using Assets;
using Entitas;
using Mono.GameMath;

public class SquadAttackOrderSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.AttackOrder.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation); } }


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
            var orientation = squadEntity.hasRotation ? squadEntity.rotation.ToQ() : Quaternion.Identity;
            var squadPosition = UnitInSquadPositioner.GetPosition(squadEntity.boxFormation, i, orientation);

            unit.ReplaceAttackOrder(squadPosition + squadEntity.attackOrder.ToV3());
        }
    }
}