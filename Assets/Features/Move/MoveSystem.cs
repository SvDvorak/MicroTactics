using Assets;
using Entitas;
using Mono.GameMath;

public class MoveSystem : IExecuteSystem, ISetPool
{
    private Group _entitiesWithOrder;

    public void SetPool(Pool pool)
    {
        _entitiesWithOrder = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Rotation, Matcher.Movement, Matcher.MoveOrder));
    }

    public void Execute()
    {
        foreach (var entity in _entitiesWithOrder.GetEntities())
        {
            var newPosition = entity.position.ToV3().MoveTowards(entity.moveOrder.Position, entity.movement.MoveSpeed);
            var newRotation = Quaternion.LookAt((entity.moveOrder.Position - entity.position.ToV3()).Normalized());
            entity.ReplacePosition(newPosition);
            entity.ReplaceRotation(newRotation);

            if(entity.position.ToV3() == entity.moveOrder.Position)
            {
                entity.RemoveMoveOrder();
            }
        }
    }
}