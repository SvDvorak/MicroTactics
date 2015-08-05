using Assets;
using Entitas;
using Mono.GameMath;

public class MoveSystem : IExecuteSystem, ISetPool
{
    private Group _entitiesWithOrder;

    public void SetPool(Pool pool)
    {
        _entitiesWithOrder = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Movement, Matcher.MoveOrder));
    }

    public void Execute()
    {
        foreach (var entity in _entitiesWithOrder.GetEntities())
        {
            var newPosition = entity.position.ToV3().MoveTowards(entity.moveOrder.ToV3(), entity.movement.MoveSpeed);

            if (entity.position.ToV3() == newPosition)
            {
                entity.RemoveMoveOrder();
            }

            entity.ReplacePosition(newPosition);
        }
    }
}