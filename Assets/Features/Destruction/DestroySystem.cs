using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

public class DestroySystem : IReactiveSystem, ISetPool
{
    private Pool _pool;

    public IMatcher trigger { get { return Matcher.Destroy; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            _pool.DestroyEntity(entity);
        }
    }
}
