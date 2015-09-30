using BehaviourMachine;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class SingleRun : DecoratorNode
    {
        public override Status Update()
        {
            if (child == null)
            {
                return Status.Error;
            }

            child.OnTick();

            owner.root.SendEvent("SUCCESS");
            return Status.Success;
        }
    }
}