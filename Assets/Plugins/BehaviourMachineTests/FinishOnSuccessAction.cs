using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Plugins.BehaviourMachineTests;
using UnityEngine;

namespace BehaviourMachine
{
    [RequireComponent(typeof (Blackboard))]
    public class FinishOnSuccessAction : InternalActionState
    {
        public override void Reset()
        {
            base.Reset();
            AddNode(typeof (SendFinishedOnSuccess));
            AddTransition(GlobalBlackboard.FINISHED);
            SaveNodes();
        }
    }
}