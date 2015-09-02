    using System.Collections.Generic;
    using System.Linq;
    using Assets;
    using Entitas;

public class AttackInteractionSystem : IReactiveSystem, ISetPool, IEnsureComponents
{
    private Group _selectedGroup;

    public TriggerOnEvent trigger { get { return Matcher.Input.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.Selected, Matcher.AttackInput); } }


    public void SetPool(Pool pool)
    {
        _selectedGroup = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Selected));
    }

    public void Execute(List<Entity> entities)
    {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;

        var selectedGroup = _selectedGroup.GetSingleEntity();
        var firstEntityHit = input.EntitiesHit.First();
        if (input.State == InputState.Release)
        {
            selectedGroup.ReplaceAttackOrder(firstEntityHit.Position);
            inputEntity.RemoveAttackInput();
        }
        else
        {
            inputEntity.ReplaceAttackInput(selectedGroup.position.ToV3(), firstEntityHit.Position);
        }
    }
}