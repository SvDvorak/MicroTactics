using System.Linq;
using Assets;
using Entitas;
using Mono.GameMath;

public class PitchFromVelocitySystem : IExecuteSystem, ISetPool
{
    private Group _pitchFromVelocityGroup;

    public void SetPool(Pool pool)
    {
        _pitchFromVelocityGroup = pool.GetGroup(Matcher.PitchFromVelocity);
    }

    public void Execute()
    {
        foreach (var toPitch in _pitchFromVelocityGroup.GetEntities())
        {
            var velocity = toPitch.velocity.ToV3();
            if (!velocity.LengthSquared().IsApproximately(0))
            {
                var newForward = Quaternion.LookAt(velocity.Normalized());
                toPitch.ReplaceRotation(newForward);
            }
        }
    }
}