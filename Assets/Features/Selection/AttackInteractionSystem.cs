﻿    using System.Collections.Generic;
    using System.Linq;
    using Entitas;

public class AttackInteractionSystem : IReactiveSystem, ISetPool
{
    private Group _selectedGroup;

    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Input, Matcher.Selected, Matcher.AttackOrder); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _selectedGroup = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.Selected));
    }

    public void Execute(List<Entity> entities)
    {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;

        var firstEntityHit = input.EntitiesHit.First();
        if (input.State == InputState.Release)
        {
            _selectedGroup.GetSingleEntity().ReplaceAttackOrder(firstEntityHit.Position);
            inputEntity.RemoveAttackOrder();
        }
    }
}