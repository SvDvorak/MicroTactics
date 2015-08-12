using System.Collections.Generic;
using System.Linq;
using Entitas;
using Vexe.Runtime.Extensions;

public class SelectSquadSystem : IReactiveSystem, ISetPool
{
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

        var selectionEntity = input.Entities.LastOrDefault(x => x.hasSelectionArea);
        if (input.State == InputState.Press)
        {
            if (selectionEntity != null)
            {
                DeselectAllSquads();
                selectionEntity.selectionArea.Parent.IsSelected(true);
            }
        }
        else if (input.State == InputState.Release)
        {
            if (selectionEntity == null)
            {
                DeselectAllSquads();
            }
        }
    }

    private void DeselectAllSquads()
    {
        _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
    }
}