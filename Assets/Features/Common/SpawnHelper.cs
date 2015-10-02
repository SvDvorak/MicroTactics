using Entitas;
using Mono.GameMath;

public class SpawnHelper
{
    public static Entity Arrow(Pool pool)
    {
        return pool.CreateEntity()
            .AddResource(Res.Arrow)
            .AddStickable(10)
            .AddDelayedDestroy(20*Simulation.FrameRate);
    }

    public static Entity Unit(Pool pool, int squadNumber, Vector3 position)
    {
        return pool.CreateEntity()
            .AddUnit(squadNumber)
            .AddHealth(100)
            .AddPosition(position)
            .AddRotation(Quaternion.Identity)
            .AddResource(Res.Unit);
    }

    public static Entity SelectedIndicator(Pool pool)
    {
        return pool.CreateEntity()
            .AddResource(Res.SelectedIndicator)
            .IsHidden(true);
    }

    public static Entity SelectionArea(Pool pool, Entity parent)
    {
        return pool.CreateEntity()
            .AddSelectionArea(parent)
            .AddResource(Res.SelectionArea);
    }
}