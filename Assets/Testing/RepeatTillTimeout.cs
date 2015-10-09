using System;
using BehaviourMachine;
using UnityEngine;

namespace Assets.Testing
{
    [NodeInfo(category = "Test/")]
    public class RequireRepeatSuccess : DecoratorNode
    {
        public float SuccessTime = 1;

        private float _timeSuccessful = 0;

        public override Status Update()
        {
            if (child == null)
            {
                return Status.Error;
            }

            child.OnTick();

            if (child.status != Status.Success)
            {
                _timeSuccessful = 0;
                return child.status;
            }

            _timeSuccessful += Time.unscaledDeltaTime;

            return _timeSuccessful > SuccessTime ? Status.Success : Status.Running;
        }
    }

    [NodeInfo(category = "Test/")]
    public class RepeatTillTimeout : DecoratorNode
    {
        public float TimeoutInSeconds = 1;

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

                if(child.status == Status.Success)
                {
                    owner.root.SendEvent(GlobalBlackboard.FINISHED);
                }
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