using Entitas;

public class MoveSystem : IExecuteSystem, ISetPool
{
    private Group _entitiesWithOrder;

    public void SetPool(Pool pool)
    {
        _entitiesWithOrder = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.MoveOrder));
    }

    public void Execute()
    {
        foreach (var entity in _entitiesWithOrder.GetEntities())
        {
            var order = entity.moveOrder;
            entity.ReplacePosition(order.x, order.y, order.z);
            entity.RemoveMoveOrder();
        }
    }
}