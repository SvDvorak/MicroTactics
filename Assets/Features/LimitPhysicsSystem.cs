using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

namespace Assets.Features
{
    public class LimitPhysicsSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.LimitPhysics.OnEntityAddedOrRemoved(); } }
        public IMatcher ensureComponents { get { return Matcher.Physics; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasLimitPhysics)
                {
                    entity.physics.RigidBody.constraints = ToConstraints(entity.limitPhysics);
                }
            }
        }

        private RigidbodyConstraints ToConstraints(LimitPhysicsComponent limitPhysics)
        {
            RigidbodyConstraints constraints = RigidbodyConstraints.None;
            if (limitPhysics.Position.X) { constraints = constraints | RigidbodyConstraints.FreezePositionX; }
            if (limitPhysics.Position.Y) { constraints = constraints | RigidbodyConstraints.FreezePositionY; }
            if (limitPhysics.Position.Z) { constraints = constraints | RigidbodyConstraints.FreezePositionZ; }
            if (limitPhysics.Rotation.X) { constraints = constraints | RigidbodyConstraints.FreezeRotationX; }
            if (limitPhysics.Rotation.Y) { constraints = constraints | RigidbodyConstraints.FreezeRotationY; }
            if (limitPhysics.Rotation.Z) { constraints = constraints | RigidbodyConstraints.FreezeRotationZ; }

            return constraints;
        }
    }
}
