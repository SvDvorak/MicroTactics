using System.Collections.Generic;
using System.Linq;
using Entitas;

public class AiAttackOrderSystem : IExecuteSystem, ISetPool
{
    private Group _ais;
    private Group _enemies;

    public void SetPool(Pool pool)
    {
        _ais = pool.GetGroup(Matcher.AllOf(Matcher.Ai, Matcher.Position));
        _enemies = pool.GetGroup(Matcher.AllOf(Matcher.Enemy, Matcher.Position));
    }

    public void Execute()
    {
        foreach (var ai in _ais.GetEntities())
        {
            var closestEnemy = GetClosestEnemyTo(ai.position);
            if(closestEnemy != null)
            {
                var enemyPos = closestEnemy.position;
                ai.AddAttackOrder(enemyPos.x, enemyPos.y, enemyPos.z);
            }
        }
    }

    private Entity GetClosestEnemyTo(PositionComponent position)
    {
        return _enemies.GetSingleEntity();
    }

    //private Maybe<Entity> GetClosestEnemyTo(Vector position)
    //{
    //    var enumerable = new List<int>().Select(x => x);
    //    //var enumerable = _enemies.GetEntities().Select(x => x);

    //    return _enemies.GetEntities()
    //        .OrderBy(x => GetDifference(x.position, position))
    //        .FirstOrDefault()
    //        .ToMaybe();
    //}

    public class EntityDistance
    {
        public Entity Entity { get; private set; }
        public float Diff { get; private set; }

        public EntityDistance(Entity entity, float diff)
        {
            Entity = entity;
            Diff = diff;
        }
    }

    private static float GetDifference(Vector p1, Vector p2)
    {
        return (p1.ToV3() - p2.ToV3()).magnitude;
    }
}