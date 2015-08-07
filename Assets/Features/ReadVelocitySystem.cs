using Assets;
using Entitas;

public class ReadVelocitySystem : IExecuteSystem, ISetPool
{
    private Group _velocityGroup;

    public void SetPool(Pool pool)
    {
        _velocityGroup = pool.GetGroup(Matcher.AllOf(Matcher.Velocity, Matcher.Physics));
    }

    public void Execute()
    {
        foreach (var entity in _velocityGroup.GetEntities())
        {
            var velocity = entity.physics.RigidBody.velocity;
            entity.ReplaceVelocity(velocity.ToV3());
        }
    }
}