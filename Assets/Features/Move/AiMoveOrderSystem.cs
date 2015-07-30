using System.Linq;
using Entitas;

public class AiMoveOrderSystem : IExecuteSystem, ISetPool
{
    private Group _aiSquads;
    private Group _positionableUnits;

    public void SetPool(Pool pool)
    {
        _aiSquads = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.Ai));
        _positionableUnits = pool.GetGroup(Matcher.AllOf(Matcher.Unit, Matcher.Position));
    }

    public void Execute()
    {
        var squadUnitGroups = _positionableUnits.GetEntities().GroupBy(x => x.unit.SquadNumber).ToList();

        foreach (var aiSquad in _aiSquads.GetEntities())
        {
            var unitsInSquad = squadUnitGroups.Find(x => x.Key == aiSquad.squad.Number);
            OrderUnitsToSquadRelativePositions(unitsInSquad, aiSquad.squad);
        }
    }

    private static void OrderUnitsToSquadRelativePositions(IGrouping<int, Entity> unitsInSquad, SquadComponent squad)
    {
        for (var i = 0; i < unitsInSquad.Count(); i++)
        {
            var unit = unitsInSquad.ElementAt(i);
            if (!unit.hasMoveOrder)
            {
                unit.AddMoveOrder(i%squad.Columns, 0, i/squad.Rows);
            }
        }
    }
}
