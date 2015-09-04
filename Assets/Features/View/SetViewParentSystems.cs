using System.Collections.Generic;
using Entitas;

namespace Assets.Features.View
{
    public class AddViewParentSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.View, Matcher.Parent).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.parent.Value.hasView)
                {
                    var parentView = entity.parent.Value.view;
                    entity.view.GameObject.transform.SetParent(parentView.GameObject.transform, true);
                }
            }
        }
    }
}