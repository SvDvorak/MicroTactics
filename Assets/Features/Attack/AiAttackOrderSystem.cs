using System.Linq;
using Assets;
using Entitas;
using Mono.GameMath;

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
            var closestEnemy = GetClosestEnemyTo(ai);
            if(closestEnemy.HasValue)
            {
                var enemyPos = closestEnemy.Value.position;
                ai.ReplaceAttackOrder(enemyPos.x, enemyPos.y, enemyPos.z);
            }
            else if (ai.hasAttackOrder)
            {
                ai.RemoveAttackOrder();
            }
        }
    }

    private Maybe<Entity> GetClosestEnemyTo(Entity aiEntity)
    {
        return _enemies.GetEntities()
            .Select(x => new { Entity = x, Diff = GetDifference(x.position.ToV3(), aiEntity.position.ToV3()) })
            .OrderBy(x => x.Diff)
            .Where(x => x.Diff < aiEntity.ai.SeeingRange)
            .Select(x => x.Entity)
            .FirstOrDefault()
            .ToMaybe();
    }

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

    private static float GetDifference(Vector3 p1, Vector3 p2)
    {
        return (p1 - p2).Length();
    }
}