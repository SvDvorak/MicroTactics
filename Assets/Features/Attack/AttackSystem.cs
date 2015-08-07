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

            var distance = (entity.position.ToV3() - entity.attackOrder.ToV3()).Length();
            var force = CalculateForce(entity.rotation, distance, 1);
            entity.AddFireArrow(entity.position.ToV3() + new Vector3(0, 4, 0), entity.rotation.ToQ(), force);
        }
    }

    private Vector3 CalculateForce(QuaternionClass targetRotation, float targetDistance, float mass)
    {
        // Algorithm taken from http://en.wikipedia.org/wiki/Trajectory_of_a_projectile
        const int fireAngle = 45;
        var verticalAimRotation = Quaternion.CreateFromAxisAngle(Vector3.Left, fireAngle);
        var fireDirection = targetRotation.ToQ() * verticalAimRotation * Vector3.Forward;
        var requiredVelocity = Mathf.Sqrt((targetDistance * Simulation.Gravity) / Mathf.Sin(MathHelper.ToRadians(fireAngle * 2)));
        var requiredForce = fireDirection * requiredVelocity * (1f/Simulation.FrameRate) * mass;
        return requiredForce;
    }
}