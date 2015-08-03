using Entitas;
using UnityEngine;

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
            var newPosition = Vector3.MoveTowards(entity.position.ToV3(), entity.moveOrder.ToV3(), entity.movement.MoveSpeed);
            entity.ReplacePosition(newPosition.x, newPosition.y, newPosition.z);
        }
    }
}