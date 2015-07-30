using System.Collections.Generic;
using Entitas;

public class AiMoveOrderSystem : IExecuteSystem, ISetPool
{
    private Group _moveableAiGroup;

    public void SetPool(Pool pool)
    {
        _moveableAiGroup = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Ai));
    }

    public void Execute()
    {
        foreach (var entity in _moveableAiGroup.GetEntities())
        {
            if (!entity.hasMoveOrder)
            {
                var pos = entity.position;
                entity.AddMoveOrder(pos.x + 1, pos.y, pos.z);
            }
        }
    }
}
