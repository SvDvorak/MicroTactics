using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ForceComponent force { get { return (ForceComponent)GetComponent(ComponentIds.Force); } }

        public bool hasForce { get { return HasComponent(ComponentIds.Force); } }

        static readonly Stack<ForceComponent> _forceComponentPool = new Stack<ForceComponent>();

        public static void ClearForceComponentPool() {
            _forceComponentPool.Clear();
        }

        public Entity AddForce(float newX, float newY, float newZ) {
            var component = _forceComponentPool.Count > 0 ? _forceComponentPool.Pop() : new ForceComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            return AddComponent(ComponentIds.Force, component);
        }

        public Entity ReplaceForce(float newX, float newY, float newZ) {
            var previousComponent = hasForce ? force : null;
            var component = _forceComponentPool.Count > 0 ? _forceComponentPool.Pop() : new ForceComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            ReplaceComponent(ComponentIds.Force, component);
            if (previousComponent != null) {
                _forceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveForce() {
            var component = force;
            RemoveComponent(ComponentIds.Force);
            _forceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherForce;

        public static AllOfMatcher Force {
            get {
                if (_matcherForce == null) {
                    _matcherForce = new Matcher(ComponentIds.Force);
                }

                return _matcherForce;
            }
        }
    }
}
