using Assets;
using Entitas;

public class ReadPhysicsSystem : IExecuteSystem, ISetPool
{
    private Group _velocityGroup;
    private Group _positionGroup;

    public void SetPool(Pool pool)
    {
        _velocityGroup = pool.GetGroup(Matcher.AllOf(Matcher.Velocity, Matcher.Physics));
        _positionGroup = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Physics));
    }

    public void Execute()
    {
        foreach (var entity in _velocityGroup.GetEntities())
        {
            var velocity = entity.physics.RigidBody.velocity;
            entity.ReplaceVelocity(velocity.ToV3());
        }

        foreach (var entity in _positionGroup.GetEntities())
        {
            entity.ReplacePosition(entity.physics.RigidBody.position.ToV3());
        }
    }
}