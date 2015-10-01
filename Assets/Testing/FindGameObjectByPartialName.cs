using BehaviourMachine;

namespace Assets.Testing
{
    [NodeInfo(category = "Action/GameObject/", icon = "GameObject")]
    public class FindGameObjectByPartialName : ActionNode
    {
        public StringVar ObjectName;
        public GameObjectVar StoreGameObject;

        public override Status Update()
        {
            StoreGameObject.Value = GameObjectExtensions.Find(ObjectName.Value, true);
            return Status.Success;
        }
    }
}