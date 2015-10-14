using System;
using BehaviourMachine;
using UnityEngine;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/", icon = "GameObject")]
    public class ClickGameObject : ActionNode
    {
        public GameObjectVar PressObject;
        [VariableInfo(nullLabel = "Use same as press")]
        public GameObjectVar ReleaseObject;
        [VariableInfo(nullLabel = "Zero vector")]
        public Vector3Var PressPosition;
        [VariableInfo(nullLabel = "Zero vector")]
        public Vector3Var ReleasePosition;

        public override Status Update()
        {
            var releaseObject = ReleaseObject.isNone ? PressObject.Value : ReleaseObject.Value;
            var pressPosition = PressPosition.isNone ? Vector3.zero : PressPosition.Value;
            var releasePosition = ReleasePosition.isNone ? Vector3.zero : ReleasePosition.Value;

            TestInput.SetTestInput()
                .Click(PressObject.Value, pressPosition)
                .Release(releaseObject, releasePosition);

            return Status.Success;
        }
    }
}