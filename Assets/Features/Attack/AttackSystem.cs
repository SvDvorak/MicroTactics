using System;
using System.Collections.Generic;
using Assets;
using Entitas;
using Mono.GameMath;
using Mathf = UnityEngine.Mathf;
using Random = Assets.Random;

public class AttackSystem : IReactiveSystem, ISetPool, IEnsureComponents
{
    private Pool _pool;

    public TriggerOnEvent trigger { get { return Matcher.AttackOrder.OnEntityAdded(); } }
    public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.Unit, Matcher.Position, Matcher.Rotation); } }


    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.hasReload)
            {
                continue;
            }

            var attackDirection = entity.attackOrder.ToV3() - entity.position.ToV3();
            LookAtTarget(entity, attackDirection);
            SpawnArrow(entity, attackDirection);
            entity.AddReload(5*Simulation.FrameRate);

            entity.RemoveAttackOrder();
        }
    }

    private static void LookAtTarget(Entity entity, Vector3 attackDirection)
    {
        entity.ReplaceRotation(Quaternion.LookAt(attackDirection.Normalized()));
    }

    private void SpawnArrow(Entity entity, Vector3 attackDirection)
    {
        var firePosition = entity.position.ToV3() + new Vector3(0, 4, 0);
        var force = CalculateForce(firePosition.Y, entity.rotation, attackDirection.Length(), 1);
        force = AddRandomAimingVariation(force);

        SpawnHelper.SpawnArrow(_pool)
            .AddPosition(firePosition)
            .AddRotation(entity.rotation.ToQ())
            .AddForce(force)
            .AddVelocity(attackDirection);
    }

    private static Vector3 CalculateForce(
        float elevation,
        QuaternionClass targetRotation,
        float targetDistance,
        float mass)
    {
        // Algorithm taken from http://physics.stackexchange.com/questions/27992/solving-for-initial-velocity-required-to-launch-a-projectile-to-a-given-destinat
        const int fireAngle = 45;
        var verticalAimRotation = Quaternion.CreateFromAxisAngle(Vector3.Left, -fireAngle);
        var fireDirection = targetRotation.ToQ()*verticalAimRotation*Vector3.Forward;
        var inRadians = MathHelper.ToRadians(fireAngle);
        var requiredVelocity = 1/Mathf.Cos(inRadians)*
                               Mathf.Sqrt(
                                   (0.5f*Simulation.Gravity*targetDistance*targetDistance)/
                                   (targetDistance*Mathf.Tan(inRadians) + elevation));
        return fireDirection*requiredVelocity*Simulation.FrameRate*mass;
    }

    private Vector3 AddRandomAimingVariation(Vector3 force)
    {
        // Algorithm taken from http://math.stackexchange.com/questions/56784/generate-a-random-direction-within-a-cone
        var normalizedForce = force.Normalized();
        var side = normalizedForce.Cross(Vector3.Up);
        var up = side.Cross(normalizedForce);

        var x = (Random.Instance.Value - 0.5f)*Mathf.Deg2Rad*5;
        var y = (Random.Instance.Value - 0.5f)*Mathf.Deg2Rad*5;
        var perturbedDirection = Mathf.Sin(y)*(Mathf.Cos(x)*side + Mathf.Sin(x)*up) + Mathf.Cos(y)*normalizedForce;
        return perturbedDirection*force.Length();
    }
}