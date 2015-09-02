using System.Collections.Generic;
using Entitas;

namespace Assets.Features.CreateSquad
{
    public class SelectionAreaAddDecoratorSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Squad, Matcher.Player).OnEntityAdded(); } }


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

            entity.AddChildTwoWay(selectionArea);
        }
    }

    public class SelectionAreaRemoveDecoratorSystem : IMultiReactiveSystem
    {
        public TriggerOnEvent[] triggers { get { return new[] { Matcher.AllOf(Matcher.Destroy, Matcher.Squad, Matcher.Player).OnEntityAdded(), Matcher.AllOf(Matcher.Squad, Matcher.Player).OnEntityRemoved() }; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                foreach (var child in entity.child.Value)
                {
                    child.IsDestroy(true);
                }
            }
        }
    }
}