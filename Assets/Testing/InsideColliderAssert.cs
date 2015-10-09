using System.Collections.Generic;
using BehaviourMachine;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Assertions.Must;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class InsideColliderAssert : ActionNode
    {
        public GameObjectVar Collider;
        public GameObjectVar ObjectToBeInside;

        // NOTE: Only handles boxes currently!
        // For more advanced forms, change bounds contains check!
        public override Status Update()
        {
            var collider = Collider.Value.GetComponent<Collider>();
            var isInside = collider.bounds.Contains(ObjectToBeInside.transform.position);
            isInside.MustBeTrue(CreateErrorMessage(collider.bounds));
            return Status.Success;
        }

        private string CreateErrorMessage(Bounds bounds)
        {
            return string.Format("{0} should be inside {1}\n Object has position {2} and collider has bounds {3}",
                ObjectToBeInside.Value, Collider, ObjectToBeInside.Value.transform.position, bounds);
        }
    }
}