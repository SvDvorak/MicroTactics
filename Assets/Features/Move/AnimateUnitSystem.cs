using System.Collections.Generic;
using Entitas;

public class AnimateUnitSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Animate, Matcher.MoveOrder).OnEntityAddedOrRemoved(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            entity.animate.Animator.SetBool("IsMoving", entity.hasMoveOrder);
        }
    }
}