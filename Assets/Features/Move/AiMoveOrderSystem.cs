using System.Collections.Generic;
using System.Linq;
using Assets;
using Entitas;
using Mono.GameMath;

public class AiMoveOrderSystem : IExecuteSystem, ISetPool
{
    private Group _aiSquads;
    private Group _enemies;

    public void SetPool(Pool pool)
    {
        _aiSquads = pool.GetGroup(Matcher.AllOf(Matcher.Squad, Matcher.Ai, Matcher.Position));
        _enemies = pool.GetGroup(Matcher.AllOf(Matcher.Enemy, Matcher.Position));
    }

    public void Execute()
    {
        var enemy = _enemies.GetSingleEntity();
        if (enemy == null)
        {
            return;
        }

        foreach (var squad in _aiSquads.GetEntities().ToList())
        {
            var squadPosition = squad.position.ToV3();
            var toEnemy = squadPosition - enemy.position.ToV3();

            if (toEnemy.Length() < 1)
            {
                var moveDirection = toEnemy.ClampLength(1);
                if (moveDirection.Length().IsApproximately(0))
                {
                    moveDirection = new Vector3(1, 0, 0);
                }

                squad.ReplaceMoveOrder(squadPosition + moveDirection, Quaternion.Identity);
            }
        }
    }
}
