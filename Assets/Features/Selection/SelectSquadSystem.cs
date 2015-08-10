using System.Collections.Generic;
using System.Linq;
using Entitas;

public class SelectSquadSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.InputPress; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void Execute(List<Entity> entities)
    {
        var inputPress = entities.SingleEntity().inputPress;

        var selectionEntity = inputPress.Entities.FirstOrDefault(x => x.hasSelectionArea);
        if (selectionEntity != null)
        {
            selectionEntity.selectionArea.Parent.IsSelected(true);
        }
    }
}