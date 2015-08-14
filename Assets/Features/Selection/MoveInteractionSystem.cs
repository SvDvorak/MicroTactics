using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;

namespace Assets.Features.Selection
{
    public class MoveInteractionSystem : IReactiveSystem, ISetPool
    {
        private Group _selectedGroup;

        public IMatcher trigger { get { return Matcher.AllOf(Matcher.Input, Matcher.Selected, Matcher.MoveOrder); } }
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
            if (input.State == InputState.Release)
            {
                MoveTo(inputEntity.moveOrder.Position, firstEntityHit.Position);
                inputEntity.RemoveMoveOrder();
            }
        }

        private void MoveTo(Vector3 position, Vector3 lookAtPoint)
        {
            var orientation = Quaternion.LookAt((lookAtPoint - position).Normalized());
            _selectedGroup.GetSingleEntity().ReplaceMoveOrder(position, orientation);
        }
    }
}