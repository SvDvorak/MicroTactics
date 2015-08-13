using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;
using Vexe.Runtime.Extensions;

public class SquadInteractionSystem : IReactiveSystem, ISetPool
{
    private bool _isMoving;
    private Group _selectedGroup;
    private Vector3 _moveStartPosition;

    public IMatcher trigger { get { return Matcher.Input; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _selectedGroup = pool.GetGroup(Matcher.Selected);
    }

    public void Execute(List<Entity> entities)
    {
        var input = entities.SingleEntity().input;

        if (input.EntitiesHit.IsEmpty())
        {
            return;
        }

        var firstEntityHit = input.EntitiesHit.First();
        var selectionEntityHit = input.EntitiesHit.LastOrDefault(x => x.Entity.hasSelectionArea);
        if (input.State == InputState.Press)
        {
            if (selectionEntityHit != null)
            {
                DeselectAllSquads();
                selectionEntityHit.Entity.selectionArea.Parent.IsSelected(true);
            }
            else
            {
                _moveStartPosition = firstEntityHit.Position;
            }
        }
        else if (input.State == InputState.Release)
        {
            var inputMoveDistance = (firstEntityHit.Position - _moveStartPosition).Length();
            if (selectionEntityHit == null)
            {
                if (inputMoveDistance < 1)
                {
                    DeselectAllSquads();
                }
                else if(_selectedGroup.Count > 0)
                {
                    _selectedGroup.GetSingleEntity().ReplaceMoveOrder(_moveStartPosition);
                }
            }
        }
    }

    private void DeselectAllSquads()
    {
        _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
    }
}