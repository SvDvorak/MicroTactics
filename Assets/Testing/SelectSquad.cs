using System.Collections.Generic;
using System.Linq;
using Assets.Features.CreateSquad;
using BehaviourMachine;
using Entitas;
using Vexe.Runtime.Extensions;

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

    [NodeInfo(category = "MicroTactics/", icon = "GameObject")]
    public class GetSquadUnits : ActionNode
    {
        public GameObjectVar SquadPlacementObject;
        public DynamicList UnitList;

        public override Status Update()
        {
            UnitList.Clear();
            var squad = SquadPlacementObject.Value.GetComponent<CreateEntityOnStart>().Entity;
            var units = squad.unitsCache.Units.Select(x => x.view.Value).ToList();
            units.Foreach(x => UnitList.Add(x));

            return Status.Success;
        }
    }
}