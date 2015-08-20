using System.Collections.Generic;
using Entitas;

namespace Assets.Features.CreateSquad
{
    public class SelectionAreaAddDecoratorSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public IMatcher trigger { get { return Matcher.AllOf(Matcher.Squad, Matcher.Player); } }
        public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                CreateSelectionArea(entity);
            }
        }

        private void CreateSelectionArea(Entity entity)
        {
            var selectionArea = _pool
                .CreateEntity()
                .AddSelectionArea(entity)
                .AddResource(Res.SelectionArea);

            entity.SetChildTwoWay(selectionArea);
        }
    }

    public class SelectionAreaRemoveDecoratorSystem : IMultiReactiveSystem
    {
        public IMatcher[] triggers { get { return new IMatcher[] { Matcher.AllOf(Matcher.Destroy, Matcher.Squad, Matcher.Player), Matcher.AllOf(Matcher.Squad, Matcher.Player) }; } }
        public GroupEventType[] eventTypes { get { return new[] { GroupEventType.OnEntityAdded, GroupEventType.OnEntityRemoved }; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.child.Value.IsDestroy(true);
            }
        }
    }
}