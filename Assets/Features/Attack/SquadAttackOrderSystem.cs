﻿using System.Collections.Generic;
using Entitas;

public class SquadAttackOrderSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.UnitsCache, Matcher.BoxFormation, Matcher.AttackOrder); } }
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
                squadPosition.x + squadEntity.attackOrder.x,
                squadPosition.y + squadEntity.attackOrder.y,
                squadPosition.z + squadEntity.attackOrder.z);
        }
    }
}