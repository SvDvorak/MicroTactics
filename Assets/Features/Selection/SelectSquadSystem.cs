using System.Collections.Generic;
using System.Linq;
using Entitas;
using Vexe.Runtime.Extensions;

public class SelectSquadSystem : IReactiveSystem, ISetPool
{
    private Group _selectedGroup;
    public IMatcher trigger { get { return Matcher.InputPress; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _selectedGroup = pool.GetGroup(Matcher.Selected);
    }

    public void Execute(List<Entity> entities)
    {
        var inputPress = entities.SingleEntity().inputPress;

        var selectionEntity = inputPress.Entities.FirstOrDefault(x => x.hasSelectionArea);
        if (selectionEntity != null)
        {
            _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
            selectionEntity.selectionArea.Parent.IsSelected(true);
        }
    }
}