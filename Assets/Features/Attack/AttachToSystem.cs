using System.Collections.Generic;
using Entitas;

namespace Assets.Features.Attack
{
    public class AttachToSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AttachTo.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var attachee = entity.attachTo.Entity;
                if (entity.hasView && attachee.hasAttachRoot)
                {
                    var attacherTransform = entity.view.Value.transform;
                    var attacheeTransform = attachee.attachRoot.Value.transform;
                    attacherTransform.SetParent(attacheeTransform);
                }

                entity.RemoveAttachTo();
            }
        }
    }
}
