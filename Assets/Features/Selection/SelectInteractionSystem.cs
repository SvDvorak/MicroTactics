using System.Collections.Generic;
using System.Linq;
using Entitas;
using Vexe.Runtime.Extensions;

namespace Assets.Features.Selection
{
    public class SelectInteractionSystem : IReactiveSystem, ISetPool, IExcludeComponents
    {
        private Group _selectedGroup;

        public IMatcher trigger { get { return Matcher.Input; } }
        public IMatcher excludeComponents { get { return Matcher.Selected; } }
        public GroupEventType eventType { get { return GroupEventType.OnEntityAdded; } }

        public void SetPool(Pool pool)
        {
            _selectedGroup = pool.GetGroup(Matcher.Selected);
        }

        public void Execute(List<Entity> entities)
        {
            var inputEntity = entities.SingleEntity();
            var input = inputEntity.input;

            var selectionEntityHit = input.EntitiesHit.LastOrDefault(x => x.Entity.hasSelectionArea);
            if (selectionEntityHit != null && input.State == InputState.Press)
            {
                DeselectAllSquads();
                selectionEntityHit.Entity.selectionArea.Parent.IsSelected(true);
                inputEntity.IsSelected(true);
            }
            else if (selectionEntityHit == null && input.State == InputState.Release)
            {
                DeselectAllSquads();
                inputEntity.IsSelected(false);
            }
        }

        private void DeselectAllSquads()
        {
            _selectedGroup.GetEntities().Foreach(x => x.IsSelected(false));
        }
    }
}