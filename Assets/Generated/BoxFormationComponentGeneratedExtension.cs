using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BoxFormationComponent boxFormation { get { return (BoxFormationComponent)GetComponent(ComponentIds.BoxFormation); } }

        public bool hasBoxFormation { get { return HasComponent(ComponentIds.BoxFormation); } }

        static readonly Stack<BoxFormationComponent> _boxFormationComponentPool = new Stack<BoxFormationComponent>();

        public static void ClearBoxFormationComponentPool() {
            _boxFormationComponentPool.Clear();
        }

        public Entity AddBoxFormation(int newColumns, int newRows, int newSpacing) {
            var component = _boxFormationComponentPool.Count > 0 ? _boxFormationComponentPool.Pop() : new BoxFormationComponent();
            component.Columns = newColumns;
            component.Rows = newRows;
            component.Spacing = newSpacing;
            return AddComponent(ComponentIds.BoxFormation, component);
        }

        public Entity ReplaceBoxFormation(int newColumns, int newRows, int newSpacing) {
            var previousComponent = hasBoxFormation ? boxFormation : null;
            var component = _boxFormationComponentPool.Count > 0 ? _boxFormationComponentPool.Pop() : new BoxFormationComponent();
            component.Columns = newColumns;
            component.Rows = newRows;
            component.Spacing = newSpacing;
            ReplaceComponent(ComponentIds.BoxFormation, component);
            if (previousComponent != null) {
                _boxFormationComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBoxFormation() {
            var component = boxFormation;
            RemoveComponent(ComponentIds.BoxFormation);
            _boxFormationComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherBoxFormation;

        public static AllOfMatcher BoxFormation {
            get {
                if (_matcherBoxFormation == null) {
                    _matcherBoxFormation = new Matcher(ComponentIds.BoxFormation);
                }

                return _matcherBoxFormation;
            }
        }
    }
}
