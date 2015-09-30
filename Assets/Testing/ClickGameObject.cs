using BehaviourMachine;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/", icon = "GameObject")]
    public class ClickGameObject : ActionNode
    {
        public GameObjectVar GameObject;

        public override Status Update()
        {
            var input = new TestInput();
            input.AddGameObjectClick(GameObject.Value);
            WilInput.Instance = input;
            return Status.Success;
        }
    }
}