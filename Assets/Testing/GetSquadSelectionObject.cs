using System.Linq;
using Assets.Features.CreateSquad;
using BehaviourMachine;
using Entitas;
using UnityEngine;

namespace Assets.Testing
{
    [NodeInfo(category = "MicroTactics/", icon = "GameObject")]
    public class GetSquadSelectionObject : ActionNode
    {
        public GameObjectVar SquadPlacementObject;
        public GameObjectVar SquadSelectionObject;

        public override Status Update()
        {
            SquadSelectionObject.Value = GetSquadSelection(SquadPlacementObject.Value);

            return Status.Success;
        }

        public static GameObject GetSquadSelection(GameObject squadPlacementObject)
        {
            var entity = squadPlacementObject.GetComponent<CreateEntityOnStart>().Entity;
            var selectionAreaEntity = entity.children.Value.Single(x => x.hasSelectionArea);
            return selectionAreaEntity.view.Value;
        }
    }
}