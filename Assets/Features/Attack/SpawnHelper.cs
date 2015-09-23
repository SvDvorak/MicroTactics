using Entitas;

public class SpawnHelper
{
    public static Entity SpawnArrow(Pool pool)
    {
        return pool.CreateEntity()
            .AddResource(Res.Arrow)
            .AddStickable(10)
            .AddDelayedDestroy(20 * Simulation.FrameRate);
    }
}