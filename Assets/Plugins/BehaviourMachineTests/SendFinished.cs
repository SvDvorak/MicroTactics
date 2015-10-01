using System.Linq;
using BehaviourMachine;

namespace Assets.Plugins.BehaviourMachineTests
{
    [NodeInfo(category = "Test/", icon = "StateMachine")]
    public class SendFinishedOnSuccess : ActionNode
    {
        public override Status Update()
        {
            var nodesButThisOne = branch.children.Where(x => x != this).ToList();
            if (nodesButThisOne.All(x => x.status == Status.Success))
            {
                owner.root.SendEvent(GlobalBlackboard.FINISHED);
            }
            return Status.Success;
        }
    }
}