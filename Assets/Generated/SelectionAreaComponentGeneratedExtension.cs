using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SelectionAreaComponent selectionArea { get { return (SelectionAreaComponent)GetComponent(ComponentIds.SelectionArea); } }

        public bool hasSelectionArea { get { return HasComponent(ComponentIds.SelectionArea); } }

        static readonly Stack<SelectionAreaComponent> _selectionAreaComponentPool = new Stack<SelectionAreaComponent>();

        public static void ClearSelectionAreaComponentPool() {
            _selectionAreaComponentPool.Clear();
        }

        public Entity AddSelectionArea(Entitas.Entity newParent) {
            var component = _selectionAreaComponentPool.Count > 0 ? _selectionAreaComponentPool.Pop() : new SelectionAreaComponent();
            component.Parent = newParent;
            return AddComponent(ComponentIds.SelectionArea, component);
        }

        public Entity ReplaceSelectionArea(Entitas.Entity newParent) {
            var previousComponent = hasSelectionArea ? selectionArea : null;
            var component = _selectionAreaComponentPool.Count > 0 ? _selectionAreaComponentPool.Pop() : new SelectionAreaComponent();
            component.Parent = newParent;
            ReplaceComponent(ComponentIds.SelectionArea, component);
            if (previousComponent != null) {
                _selectionAreaComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSelectionArea() {
            var component = selectionArea;
            RemoveComponent(ComponentIds.SelectionArea);
            _selectionAreaComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherSelectionArea;

        public static AllOfMatcher SelectionArea {
            get {
                if (_matcherSelectionArea == null) {
                    _matcherSelectionArea = new Matcher(ComponentIds.SelectionArea);
                }

                return _matcherSelectionArea;
            }
        }
    }
}
