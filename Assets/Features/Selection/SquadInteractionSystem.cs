using System.Collections.Generic;
using System.Linq;
using Entitas;
using Mono.GameMath;
using Vexe.Runtime.Extensions;

public class SquadInteractionSystem : IReactiveSystem, ISetPool
{
    private Group _selectedGroup;
    private Vector3 _moveStartPosition;
    private bool _isAttacking;

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

        var selectionEntityHit = input.EntitiesHit.LastOrDefault(x => x.Entity.hasSelectionArea);

        if (selectionEntityHit != null)
        {
            if (input.State == InputState.Press)
            {
                _isAttacking = true;
                SetSelected(selectionEntityHit.Entity.selectionArea.Parent);
            }
            else if(input.State == InputState.Release)
            {
                Attack(selectionEntityHit.Position);
            }
        }
        else
        {
            var firstEntityHit = input.EntitiesHit.First();
            if (input.State == InputState.Press)
            {
                _moveStartPosition = firstEntityHit.Position;
            }
            else if (input.State == InputState.Release)
            {
                var inputMoveDistance = (firstEntityHit.Position - _moveStartPosition).Length();
                if (inputMoveDistance < 1)
                {
                    DeselectAllSquads();
                }
                else if (_selectedGroup.Count > 0)
                {
                    if (_isAttacking)
                    {
                        Attack(firstEntityHit.Position);
                    }
                    else
                    {
                        MoveTo(_moveStartPosition);
                    }
                }
            }
        }
    }

    private void SetSelected(Entity squad)
    {
        DeselectAllSquads();
        squad.IsSelected(true);
    }

    private void MoveTo(Vector3 position)
    {
        _selectedGroup.GetSingleEntity().ReplaceMoveOrder(position, Quaternion.Identity);
    }

    private void Attack(Vector3 position)
    {
        _selectedGroup.GetSingleEntity().ReplaceAttackOrder(position);
        _isAttacking = false;
    }

    private void DeselectAllSquads()
    {
        _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
    }
}