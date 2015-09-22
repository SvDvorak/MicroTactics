using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class DestroyComponent : IComponent
{
}

public static class DestroyExtensions
{
    public static Entity RecursiveDestroy(this Entity entity)
    {
        entity.IsDestroy(true);

        if (entity.hasChildren)
        {
            foreach (var child in entity.children.Value)
            {
                RecursiveDestroy(child);
            }
        }

        return entity;
    }
}
