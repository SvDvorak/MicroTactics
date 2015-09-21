using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ParentComponent parent { get { return (ParentComponent)GetComponent(ComponentIds.Parent); } }

        public bool hasParent { get { return HasComponent(ComponentIds.Parent); } }

        static readonly Stack<ParentComponent> _parentComponentPool = new Stack<ParentComponent>();

        public static void ClearParentComponentPool() {
            _parentComponentPool.Clear();
        }

        public Entity AddParent(Entitas.Entity newValue) {
            var component = _parentComponentPool.Count > 0 ? _parentComponentPool.Pop() : new ParentComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Parent, component);
        }

        public Entity ReplaceParent(Entitas.Entity newValue) {
            var previousComponent = hasParent ? parent : null;
            var component = _parentComponentPool.Count > 0 ? _parentComponentPool.Pop() : new ParentComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Parent, component);
            if (previousComponent != null) {
                _parentComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveParent() {
            var component = parent;
            RemoveComponent(ComponentIds.Parent);
            _parentComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherParent;

        public static AllOfMatcher Parent {
            get {
                if (_matcherParent == null) {
                    _matcherParent = new Matcher(ComponentIds.Parent);
                }

                return _matcherParent;
            }
        }
    }
}
