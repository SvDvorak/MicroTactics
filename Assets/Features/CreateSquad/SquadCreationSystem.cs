using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class SquadCreationSystem : IReactiveSystem, ISetPool
{
    private Pool _pool;

    public IMatcher trigger { get { return Matcher.Squad; } }
    public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute(List<Entity> entities)
    {
        var singleEntity = entities.SingleEntity();

        for (int y = 0; y < singleEntity.squad.Rows; y++)
        {
            for (int x = 0; x < singleEntity.squad.Columns; x++)
            {
                _pool.CreateEntity();
            }
        }
    }
}