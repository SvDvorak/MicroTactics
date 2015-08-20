using Entitas;

public class ChildComponent : IComponent
{
    public Entity Value;
}

public static class EntityChildExtensions
{
    public static void SetChildTwoWay(this Entity entity, Entity child)
    {
        entity.AddChild(child);
        child.AddParent(entity);
    }
}