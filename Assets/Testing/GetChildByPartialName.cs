using BehaviourMachine;

namespace Assets.Testing
{
    [NodeInfo(category = "Action/GameObject/", icon = "GameObject")]
    public class GetChildByPartialName : ActionNode
    {
        public GameObjectVar GameObject;
        public StringVar Name;
        public GameObjectVar StoreChild;

        public override Status Update()
        {
            StoreChild.Value = GameObject.Value.GetChild(Name.Value, true);
            return Status.Success;
        }
    }
}