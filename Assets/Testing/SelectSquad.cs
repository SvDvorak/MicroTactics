using System.Collections.Generic;
using System.Linq;
using Assets.Features.CreateSquad;
using BehaviourMachine;
using Entitas;

namespace Assets.Testing
{
    [NodeInfo(category = "MicroTactics/", icon = "GameObject")]
    public class SelectSquad : ActionNode
    {
        public GameObjectVar SquadPlacementObject;

        public override Status Update()
        {
            var entity = SquadPlacementObject.Value.GetComponent<CreateEntityOnStart>().Entity;
            var selectionArea = entity.children.Value.Single(x => x.hasSelectionArea);

            ClickGameObject.PerformClickDrag(selectionArea.view.Value);

            return Status.Success;
        }
    }
}