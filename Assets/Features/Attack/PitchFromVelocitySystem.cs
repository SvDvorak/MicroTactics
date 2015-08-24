using System.Linq;
using Assets;
using Entitas;
using Mono.GameMath;

public class PitchFromVelocitySystem : IExecuteSystem, ISetPool
{
    private Group _arrowGroup;

    public void SetPool(Pool pool)
    {
        _arrowGroup = pool.GetGroup(Matcher.AllOf(Matcher.Physics, Matcher.Rotation, Matcher.Velocity));
    }

    public void Execute()
    {
        foreach (var arrow in _arrowGroup.GetEntities().Where(x => !x.hasCollision))
        {
            var velocity = arrow.velocity.ToV3();
            if (!velocity.LengthSquared().IsApproximately(0))
            {
                var newForward = Quaternion.LookAt(velocity.Normalized());
                arrow.ReplaceRotation(newForward);
            }
        }
    }
}