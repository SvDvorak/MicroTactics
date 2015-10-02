using System.Collections.Generic;
using Entitas;

namespace Assets.Features.Attack
{
    public class ClearCollisionsSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.Collision.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.RemoveCollision();
            }
        }
    }
}
