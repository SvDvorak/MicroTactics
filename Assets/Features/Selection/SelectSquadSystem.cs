using System.Collections.Generic;
using System.Linq;
using Entitas;
using Vexe.Runtime.Extensions;

public class SelectSquadSystem : IReactiveSystem, ISetPool
{
    private bool _isMoving;
    private Group _selectedGroup;
    public IMatcher trigger { get { return Matcher.Input; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _selectedGroup = pool.GetGroup(Matcher.Selected);
    }

    public void Execute(List<Entity> entities)
    {
        var input = entities.SingleEntity().input;

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
                _isMoving = true;
            }
        }
        else if (input.State == InputState.Release)
        {
            if (selectionEntityHit == null && !_isMoving)
            {
                DeselectAllSquads();
            }
            else if (_isMoving)
            {
                _selectedGroup.GetSingleEntity().AddMoveOrder(input.EntitiesHit.First().Position);
            }
        }
    }

    private void DeselectAllSquads()
    {
        _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
    }
}