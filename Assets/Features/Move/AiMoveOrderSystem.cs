using System.Collections.Generic;
using System.Linq;
using Entitas;

public class AiMoveOrderSystem : IExecuteSystem, ISetPool
{
    private Group _aiSquads;

    public void SetPool(Pool pool)
    {
        _aiSquads = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.Ai));
    }

    public void Execute()
    {
        var squadsWithoutOrders = _aiSquads.GetEntities().Where(x => !x.hasMoveOrder).ToList();
        for (var i = 0; i < squadsWithoutOrders.Count(); i++)
        {
            squadsWithoutOrders[i].AddMoveOrder(i, 0, 0);
        }
    }
}
