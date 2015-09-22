using System.Collections.Generic;
using Entitas;

namespace Assets.Features.Attack
{
    public class CollisionDamageSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.Collision.OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.Health; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var newHealth = entity.health.Value - entity.collision.RelativeVelocity.Length();
                if (newHealth > 0)
                {
                    entity.ReplaceHealth(newHealth);
                }
                else
                {
                    entity
                        .RecursiveDestroy()
                        .IsKeepView(true);
                }
            }
        }
    }
}
