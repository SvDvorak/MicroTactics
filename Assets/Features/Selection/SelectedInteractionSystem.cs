using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;
using Vexe.Runtime.Extensions;

namespace Assets.Features.Selection
{
    public class SelectedInteractionSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Group _selectedGroup;
        private Vector3 _pressStartPosition;
        private bool _isAttacking;
        private bool _isDoingDrag;
        private bool _leftState;

        public IMatcher trigger { get { return Matcher.Input; } }
        public IMatcher ensureComponents { get { return Matcher.Selected; } }
        public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

        public void SetPool(Pool pool)
        {
            _selectedGroup = pool.GetGroup(Matcher.Selected);
        }

        public void Execute(List<Entity> entities)
        {
            var inputEntity = entities.SingleEntity();
            var input = inputEntity.input;

            if (inputEntity.hasMoveInput || inputEntity.hasAttackInput)
            {
                return;
            }

            if (_leftState)
            {
                _isDoingDrag = false;
                _leftState = false;
            }

            var selectionEntityHit = input.EntitiesHit.LastOrDefault(x => x.Entity.hasSelectionArea);
            var firstEntityHit = input.EntitiesHit.First();
            if (input.State == InputState.Press)
            {
                ReadyAttackOrMove(firstEntityHit, selectionEntityHit);
            }
            else if (input.State == InputState.Release)
            {
                if(selectionEntityHit == null)
                {
                    DeselectAllSquads(inputEntity);
                }
                _isDoingDrag = false;
            }
            else if(_isDoingDrag && input.State == InputState.Hover)
            {
                var inputMoveDistance = (firstEntityHit.Position - _pressStartPosition).Length();
                if (inputMoveDistance > 1)
                {
                    DoAttackOrMove(inputEntity, firstEntityHit);
                }
            }
        }

        private void ReadyAttackOrMove(EntityHit firstEntityHit, EntityHit selectionEntityHit)
        {
            _isDoingDrag = true;
            _pressStartPosition = firstEntityHit.Position;

            _isAttacking = selectionEntityHit != null;
        }

        private void DoAttackOrMove(Entity inputEntity, EntityHit firstEntityHit)
        {
            if (_isAttacking)
            {
                inputEntity.AddAttackInput(Vector3.Zero, firstEntityHit.Position);
            }
            else
            {
                inputEntity.AddMoveInput(_pressStartPosition, firstEntityHit.Position);
            }
            _leftState = true;
        }

        private void DeselectAllSquads(Entity inputEntity)
        {
            _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
            inputEntity.IsSelected(false);
        }
    }
}