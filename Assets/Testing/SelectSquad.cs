using BehaviourMachine;

namespace Assets.Testing
{
    [NodeInfo(category = "MicroTactics/", icon = "GameObject")]
    public class SelectSquad : ActionNode
    {
        public GameObjectVar SquadPlacementObject;

        public override Status Update()
        {
            var squadSelection = GetSquadSelectionObject.GetSquadSelection(SquadPlacementObject.Value);

            TestInput.SetTestInput().Click(squadSelection);

            return Status.Success;
        }
    }
}