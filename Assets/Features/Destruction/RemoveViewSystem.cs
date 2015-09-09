using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class RemoveViewSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Destroy, Matcher.View).OnEntityAdded(); } }


    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            UnityEngine.Object.Destroy(entity.view.Value);
        }
    }
}
