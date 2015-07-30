using System.Collections.Generic;
using Entitas;

public class AiMoveSystem : IReactiveSystem
{
    public IMatcher trigger { get { return Matcher.MoveOrder; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityRemoved; } }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var pos = entity.position.Position;
            entity.AddMoveOrder(new Vector(pos.x + 1, pos.y, pos.z));
        }
    }
}
