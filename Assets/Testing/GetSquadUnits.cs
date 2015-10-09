using System.Linq;
using Assets.Features.CreateSquad;
using BehaviourMachine;
using Vexe.Runtime.Extensions;

namespace Assets.Testing
{
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