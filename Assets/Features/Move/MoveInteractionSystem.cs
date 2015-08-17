using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;

namespace Assets.Features.Selection
{
    public class MoveInteractionSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Group _selectedGroup;

        public IMatcher trigger { get { return Matcher.Input; } }
        public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.Selected, Matcher.MoveInput); } }
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
            inputEntity.ReplaceMoveInput(inputEntity.moveInput.Start, firstEntityHit.Position);

            if (input.State == InputState.Release)
            {
                MoveTo(inputEntity.moveInput);
                inputEntity.RemoveMoveInput();
            }
        }

        private void MoveTo(MoveInputComponent moveInput)
        {
            var orientation = Quaternion.LookAt((moveInput.Target - moveInput.Start).Normalized());
            _selectedGroup.GetSingleEntity().ReplaceMoveOrder(moveInput.Start, orientation);
        }
    }
}