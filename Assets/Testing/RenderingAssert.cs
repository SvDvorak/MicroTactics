using System;
using BehaviourMachine;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class RenderingAssert : ActionNode
    {
        public GameObjectVar Object;
        public bool ShouldBeRendering;

        public override Status Update()
        {
            var isRendering = Object.Value.GetComponent<MeshRenderer>().enabled;
            isRendering.MustBeEqual(ShouldBeRendering, ExceptionMessage);
            return Status.Success;
        }

        private string ExceptionMessage { get { return Object.Value + " should " + (ShouldBeRendering ? "" : "not ") + "be rendering"; } }
    }
}