using System.Linq;
using Assets;
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
                .Select(unit => ToVector2(unit.position))
                .ToList();

            var hullPoints = ConvexHullCalculator
                .Calculate(unitPositions)
                .Select(x => AddPadding(x, ToVector2(squad.position)))
                .ToList();

            entity.ReplaceBoundingMesh(hullPoints);
        }
    }

    private static Vector2 ToVector2(PositionComponent position)
    {
        return new Vector2(position.x, position.z);
    }

    private Vector2 AddPadding(Vector2 position, Vector2 centerPosition)
    {
        return position + (position - centerPosition).Normalized() * Padding;
    }
}