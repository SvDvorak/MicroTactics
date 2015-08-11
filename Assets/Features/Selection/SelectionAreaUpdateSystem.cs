using System.Linq;
using Assets.Features;
using Entitas;
using Mono.GameMath;

public class SelectionAreaUpdateSystem : IExecuteSystem, ISetPool
{
    public float Padding { get; set; }

    private Group _selectionAreasGroup;

    public SelectionAreaUpdateSystem()
    {
        Padding = 2;
    }

    public void SetPool(Pool pool)
    {
        _selectionAreasGroup = pool.GetGroup(Matcher.SelectionArea);
    }

    public void Execute()
    {
        foreach (var entity in _selectionAreasGroup.GetEntities())
        {
            var squad = entity.selectionArea.Parent;
            var unitPositions = squad.unitsCache.Units
                .Select(unit => new Vector2(unit.position.x, unit.position.z))
                .ToList();

            var hullPoints = ConvexHullCalculator
                .Calculate(unitPositions)
                .Select(x => AddPadding(x))
                .ToList();

            entity.ReplaceBoundingMesh(hullPoints);
        }
    }

    private Vector2 AddPadding(Vector2 position)
    {
        return position + position.Normalized() * Padding;
    }
}