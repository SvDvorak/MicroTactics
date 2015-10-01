using System;
using BehaviourMachine;
using UnityEngine;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class RepeatTillTimeout : DecoratorNode
    {
        public float TimeoutInSeconds;

        private float _startTime;

        public override void Start()
        {
            _startTime = Time.unscaledTime;
            base.Start();
        }

        public override Status Update()
        {
            if (child == null)
            {
                return Status.Error;
            }

            try
            {
                child.OnTick();

                owner.root.SendEvent(GlobalBlackboard.FINISHED);
                return child.status;
            }
            catch(Exception ex)
            {
                var isWithinTimeout = Time.unscaledTime - _startTime > TimeoutInSeconds;
                if(isWithinTimeout)
                {
                    throw ex;
                }
            }

            return Status.Running;
        }
    }
}