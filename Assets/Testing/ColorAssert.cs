using BehaviourMachine;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class ColorAssert : ActionNode
    {
        public GameObjectVar Object;
        public ColorVar ExpectedColor;

        public override Status Update()
        {
            var actualColor = Object.Value.GetComponent<MeshRenderer>().sharedMaterial.color;
            actualColor.MustBeEqual(ExpectedColor.Value);
            return Status.Success;
        }
    }
}