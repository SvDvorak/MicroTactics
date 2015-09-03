using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.Features.Move
{
    public class RemoveMoveOrderSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.Position.OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.MoveOrder; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities.Where(entity => entity.position.ToV3().Equals(entity.moveOrder.Position, 0.01f)))
            {
                entity.RemoveMoveOrder();
            }
        }
    }
}