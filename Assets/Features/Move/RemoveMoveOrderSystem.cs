using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.Features.Move
{
    public class RemoveMoveOrderSystem : IReactiveSystem
    {
        public IMatcher trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.MoveOrder); } }
        public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities.Where(entity => entity.position.ToV3() == entity.moveOrder.Position))
            {
                entity.RemoveMoveOrder();
            }
        }
    }
}