using BehaviourMachine;
using UnityEngine;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/", icon = "GameObject")]
    public class ClickGameObject : ActionNode
    {
        public GameObjectVar GameObject;
        public Vector3Var Press;
        public Vector3Var Release;

        public override Status Update()
        {
            var pressPosition = Press.isNone ? Vector3.zero : Press.Value;
            var releasePosition = Release.isNone ? Vector3.zero : Release.Value;
            PerformClickDrag(GameObject.Value, pressPosition, releasePosition);
            return Status.Success;
        }

        public static void PerformClickDrag(GameObject gameObject, Vector3 pressPosition = new Vector3(), Vector3 releasePosition = new Vector3())
        {
            var input = new TestInput();

            input.AddMouseDown(gameObject, pressPosition);
            input.AddMouseHover(gameObject, releasePosition);
            input.AddMouseUp(gameObject, releasePosition);

            WilInput.Instance = input;
        }
    }
}