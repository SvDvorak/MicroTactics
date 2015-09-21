using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public StickableComponent stickable { get { return (StickableComponent)GetComponent(ComponentIds.Stickable); } }

        public bool hasStickable { get { return HasComponent(ComponentIds.Stickable); } }

        static readonly Stack<StickableComponent> _stickableComponentPool = new Stack<StickableComponent>();

        public static void ClearStickableComponentPool() {
            _stickableComponentPool.Clear();
        }

        public Entity AddStickable(float newRequiredVelocity) {
            var component = _stickableComponentPool.Count > 0 ? _stickableComponentPool.Pop() : new StickableComponent();
            component.RequiredVelocity = newRequiredVelocity;
            return AddComponent(ComponentIds.Stickable, component);
        }

        public Entity ReplaceStickable(float newRequiredVelocity) {
            var previousComponent = hasStickable ? stickable : null;
            var component = _stickableComponentPool.Count > 0 ? _stickableComponentPool.Pop() : new StickableComponent();
            component.RequiredVelocity = newRequiredVelocity;
            ReplaceComponent(ComponentIds.Stickable, component);
            if (previousComponent != null) {
                _stickableComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveStickable() {
            var component = stickable;
            RemoveComponent(ComponentIds.Stickable);
            _stickableComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherStickable;

        public static AllOfMatcher Stickable {
            get {
                if (_matcherStickable == null) {
                    _matcherStickable = new Matcher(ComponentIds.Stickable);
                }

                return _matcherStickable;
            }
        }
    }
}
