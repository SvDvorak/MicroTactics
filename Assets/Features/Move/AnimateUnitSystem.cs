using System.Collections.Generic;
using Entitas;

public class AnimateUnitSystem : IReactiveSystem, IEnsureComponents
{
    public TriggerOnEvent trigger { get { return Matcher.MoveOrder.OnEntityAddedOrRemoved(); } }
    public IMatcher ensureComponents { get { return Matcher.Animate; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.animate.Animator.SetBool("IsMoving", entity.hasMoveOrder);
        }
    }
}