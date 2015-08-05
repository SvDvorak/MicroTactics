using System.Collections.Generic;
using Assets;
using Entitas;
using UnityEngine;

public class AttackSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.Rotation, Matcher.AttackOrder); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var distance = (entity.position.ToV3() - entity.attackOrder.ToV3()).Length();
            var force = CalculateForce(entity.rotation, distance, 1);
            entity.AddFireArrow(entity.position, entity.rotation, new VectorClass(force.x, force.y, force.z));
        }

        //var arrow = (Rigidbody)Instantiate(ArrowTemplate, ArrowSpawnPoint.position, transform.rotation);

        //var requiredForce = CalculateForce(transform.rotation, _aimTarget.magnitude, arrow.mass);
        //arrow.AddForce(requiredForce);
    }

    private Vector3 CalculateForce(QuaternionClass targetRotation, float targetDistance, float mass)
    {
        // Algorithm taken from http://en.wikipedia.org/wiki/Trajectory_of_a_projectile
        const int fireAngle = 45;
        //var verticalAimRotation = Quaternion.AngleAxis(fireAngle, Vector3.left);
        //var fireDirection = targetRotation.ToQ() * verticalAimRotation * Vector3.forward;
        //var requiredVelocity = Mathf.Sqrt((targetDistance * Physics.gravity.magnitude) / Mathf.Sin(Mathf.Deg2Rad * fireAngle * 2));
        //var requiredForce = fireDirection * requiredVelocity * (1 / Time.fixedDeltaTime) * mass;
        //return requiredForce;
        return new Vector3();
    }
}