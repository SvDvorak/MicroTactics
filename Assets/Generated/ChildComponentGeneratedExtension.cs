using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ChildComponent child { get { return (ChildComponent)GetComponent(ComponentIds.Child); } }

        public bool hasChild { get { return HasComponent(ComponentIds.Child); } }

        static readonly Stack<ChildComponent> _childComponentPool = new Stack<ChildComponent>();

        public static void ClearChildComponentPool() {
            _childComponentPool.Clear();
        }

        public Entity AddChild(System.Collections.Generic.List<Entitas.Entity> newValue) {
            var component = _childComponentPool.Count > 0 ? _childComponentPool.Pop() : new ChildComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Child, component);
        }

        public Entity ReplaceChild(System.Collections.Generic.List<Entitas.Entity> newValue) {
            var previousComponent = hasChild ? child : null;
            var component = _childComponentPool.Count > 0 ? _childComponentPool.Pop() : new ChildComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Child, component);
            if (previousComponent != null) {
                _childComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveChild() {
            var component = child;
            RemoveComponent(ComponentIds.Child);
            _childComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherChild;

        public static AllOfMatcher Child {
            get {
                if (_matcherChild == null) {
                    _matcherChild = new Matcher(ComponentIds.Child);
                }

                return _matcherChild;
            }
        }
    }
}
