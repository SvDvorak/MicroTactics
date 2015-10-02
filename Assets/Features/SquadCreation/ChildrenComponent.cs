using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ChildrenComponent : IComponent
{
    public List<Entity> Value;
}

public static class EntityChildrenExtensions
{
    private static Entity AddChild(this Entity parent, Entity child)
    {
        if (parent.hasChildren)
        {
            parent.ReplaceChildren(parent.children.Value.Concat(child.AsList()).ToList());
        }
        else
        {
            parent.AddChildren(child.AsList());
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