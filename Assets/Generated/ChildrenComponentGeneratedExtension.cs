using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ChildrenComponent children { get { return (ChildrenComponent)GetComponent(ComponentIds.Children); } }

        public bool hasChildren { get { return HasComponent(ComponentIds.Children); } }

        static readonly Stack<ChildrenComponent> _childrenComponentPool = new Stack<ChildrenComponent>();

        public static void ClearChildrenComponentPool() {
            _childrenComponentPool.Clear();
        }

        public Entity AddChildren(System.Collections.Generic.List<Entitas.Entity> newValue) {
            var component = _childrenComponentPool.Count > 0 ? _childrenComponentPool.Pop() : new ChildrenComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Children, component);
        }

        public Entity ReplaceChildren(System.Collections.Generic.List<Entitas.Entity> newValue) {
            var previousComponent = hasChildren ? children : null;
            var component = _childrenComponentPool.Count > 0 ? _childrenComponentPool.Pop() : new ChildrenComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Children, component);
            if (previousComponent != null) {
                _childrenComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveChildren() {
            var component = children;
            RemoveComponent(ComponentIds.Children);
            _childrenComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherChildren;

        public static IMatcher Children {
            get {
                if (_matcherChildren == null) {
                    _matcherChildren = Matcher.AllOf(ComponentIds.Children);
                }

                return _matcherChildren;
            }
        }
    }
}
