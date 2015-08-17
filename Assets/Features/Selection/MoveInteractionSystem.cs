﻿using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;

namespace Assets.Features.Selection
{
    public class MoveInteractionSystem : IReactiveSystem, ISetPool
    {
        private Group _selectedGroup;

        public IMatcher trigger { get { return Matcher.AllOf(Matcher.Input, Matcher.Selected, Matcher.MoveInput); } }
        public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

        public void SetPool(Pool pool)
        {
            _selectedGroup = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.Selected));
        }

        public void Execute(List<Entity> entities)
        {
            var inputEntity = entities.SingleEntity();
            var input = inputEntity.input;

            var firstEntityHit = input.EntitiesHit.First();
            inputEntity.ReplaceMoveInput(inputEntity.moveInput.StartPosition, firstEntityHit.Position);

            if (input.State == InputState.Release)
            {
                MoveTo(inputEntity.moveInput);
                inputEntity.RemoveMoveInput();
            }
        }

        private void MoveTo(MoveInputComponent moveInput)
        {
            var orientation = Quaternion.LookAt((moveInput.TargetPosition - moveInput.StartPosition).Normalized());
            _selectedGroup.GetSingleEntity().ReplaceMoveOrder(moveInput.StartPosition, orientation);
        }
    }
}