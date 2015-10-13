using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.Features.Attack
{
    public class AttachToSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.AttachTo.OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.View, Matcher.AttachRoot); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var attachee = entity.attachTo.Entity;

                if (entity.hasPhysics && attachee.hasPhysics)
                {
                    var physicsGameObject = entity.attachRoot.Value.gameObject;
                    var fixedJoint = physicsGameObject.AddComponent<FixedJoint>();
                    fixedJoint.connectedBody = attachee.physics.RigidBody;
                }
                //else
                //{
                //    var attacherTransform = entity.view.Value.transform;
                //    var attacheeTransform = attachee.attachRoot.Value.transform;
                //    attacherTransform.transform.SetParent(attacheeTransform);
                //}

                entity.RemoveAttachTo();
            }
        }
    }
}
