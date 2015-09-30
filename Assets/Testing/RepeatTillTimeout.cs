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

                owner.root.SendEvent("SUCCESS");
                return child.status;
            }
            catch (Exception)
            {
                if(Time.unscaledTime - _startTime > TimeoutInSeconds)
                {
                    throw;
                }
            }

            return Status.Running;
        }
    }
}