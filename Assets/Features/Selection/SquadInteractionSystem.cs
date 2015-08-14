using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;
using Vexe.Runtime.Extensions;

public class SquadInteractionSystem : IReactiveSystem, ISetPool
{
    private Group _selectedGroup;
    private Interaction _interaction;

    public IMatcher trigger { get { return Matcher.AllOf(Matcher.Input); } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _selectedGroup = pool.GetGroup(Matcher.Selected);
        _interaction = new Unselected(_selectedGroup);
    }

    public void Execute(List<Entity> entities)
    {
        var input = entities.SingleEntity().input;

        if (input.EntitiesHit.IsEmpty())
        {
            return;
        }

        _interaction = _interaction.OnEntitiesHit(input);
    }

    private class Interaction
    {
        protected readonly Group SelectedGroup;

        protected Interaction(Group selectedGroup)
        {
            SelectedGroup = selectedGroup;
        }

        public virtual Interaction OnEntitiesHit(InputComponent input)
        {
            return this;
        }

        protected void DeselectAllSquads()
        {
            SelectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
        }
    }

    private class Unselected : Interaction
    {
        public Unselected(Group selectedGroup) : base(selectedGroup)
        {
        }

        public override Interaction OnEntitiesHit(InputComponent input)
        {
            var selectionEntityHit = input.EntitiesHit.LastOrDefault(x => x.Entity.hasSelectionArea);
            if (input.State == InputState.Press && selectionEntityHit != null)
            {
                DeselectAllSquads();
                selectionEntityHit.Entity.selectionArea.Parent.IsSelected(true);
                return new Selected(SelectedGroup).OnEntitiesHit(input);
            }

            return this;
        }
    }

    private class Selected : Interaction
    {
        private Vector3 _moveStartPosition;
        private Interaction _probableInteraction;

        public Selected(Group selectedGroup) : base(selectedGroup)
        {
        }

        public override Interaction OnEntitiesHit(InputComponent input)
        {
            var selectionEntityHit = input.EntitiesHit.LastOrDefault(x => x.Entity.hasSelectionArea);
            var firstEntityHit = input.EntitiesHit.First();
            if (input.State == InputState.Press)
            {
                _moveStartPosition = firstEntityHit.Position;

                _probableInteraction = selectionEntityHit != null
                    ? (Interaction) new Attacking(SelectedGroup)
                    : new Moving(SelectedGroup, _moveStartPosition);
            }

            var inputMoveDistance = (firstEntityHit.Position - _moveStartPosition).Length();
            if (inputMoveDistance > 1)
            {
                return _probableInteraction.OnEntitiesHit(input);
            }

            if (selectionEntityHit == null && input.State == InputState.Release)
            {
                DeselectAllSquads();
                return new Unselected(SelectedGroup);
            }

            return this;
        }
    }

    private class Attacking : Interaction
    {
        public Attacking(Group selectedGroup) : base(selectedGroup)
        {
        }

        public override Interaction OnEntitiesHit(InputComponent input)
        {
            var firstEntityHit = input.EntitiesHit.First();
            if (input.State == InputState.Release)
            {
                SelectedGroup.GetSingleEntity().ReplaceAttackOrder(firstEntityHit.Position);
                return new Selected(SelectedGroup);
            }

            return this;
        }
    }

    private class Moving : Interaction
    {
        private readonly Vector3 _moveStartPosition;

        public Moving(Group selectedGroup, Vector3 moveStartPosition) : base(selectedGroup)
        {
            _moveStartPosition = moveStartPosition;
        }

        public override Interaction OnEntitiesHit(InputComponent input)
        {
            var firstEntityHit = input.EntitiesHit.First();
            if (input.State == InputState.Release)
            {
                MoveTo(_moveStartPosition, firstEntityHit.Position);
                return new Selected(SelectedGroup);
            }

            return this;
        }

        private void MoveTo(Vector3 position, Vector3 lookAtPoint)
        {
            var orientation = Quaternion.LookAt((lookAtPoint - position).Normalized());
            SelectedGroup.GetSingleEntity().ReplaceMoveOrder(position, orientation);
        }
    }
}