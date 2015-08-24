using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ChildComponent : IComponent
{
    public List<Entity> Value;
}

public static class EntityChildExtensions
{
    private static Entity AddChild(this Entity parent, Entity child)
    {
        if (parent.hasChild)
        {
            parent.ReplaceChild(parent.child.Value.Concat(child.AsList()).ToList());
        }
        else
        {
            parent.AddChild(child.AsList());
        }

        return parent;
    }

    public static Entity AddChildTwoWay(this Entity parent, Entity child)
    {
        parent.AddChild(child);
        child.AddParent(parent);
        return parent;
    }
}