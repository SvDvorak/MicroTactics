using System;
using System.Collections.Generic;
using Assets;
using Entitas;
using Mono.GameMath;
using UnityEngine;
using Quaternion = Mono.GameMath.Quaternion;
using Vector3 = Mono.GameMath.Vector3;

public class AttackSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.Rotation, Matcher.AttackOrder); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.hasFireArrow)
            {
                continue;
            }

            var firePosition = entity.position.ToV3() + new Vector3(0, 4, 0);
            var distance = (entity.attackOrder.ToV3() - entity.position.ToV3()).Length();
            var force = CalculateForce(firePosition.Y, entity.rotation, distance, 1);
            entity.AddFireArrow(firePosition, entity.rotation.ToQ(), force);
        }
    }

    private Vector3 CalculateForce(float elevation, QuaternionClass targetRotation, float targetDistance, float mass)
    {
        // Algorithm taken from http://physics.stackexchange.com/questions/27992/solving-for-initial-velocity-required-to-launch-a-projectile-to-a-given-destinat
        int fireAngle = 45;
        var verticalAimRotation = Quaternion.CreateFromAxisAngle(Vector3.Left, -fireAngle);
        var fireDirection = targetRotation.ToQ() * verticalAimRotation * Vector3.Forward;
        var inRadians = MathHelper.ToRadians(fireAngle);
        var requiredVelocity = 1 / Mathf.Cos(inRadians) *
                               Mathf.Sqrt(
                                   (0.5f * Simulation.Gravity * targetDistance * targetDistance) /
                                   (targetDistance * Mathf.Tan(inRadians) + elevation));
        var requiredForce = fireDirection * requiredVelocity * Simulation.FrameRate * mass;
        return requiredForce;
    }
}