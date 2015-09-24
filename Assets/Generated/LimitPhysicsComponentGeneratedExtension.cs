using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public LimitPhysicsComponent limitPhysics { get { return (LimitPhysicsComponent)GetComponent(ComponentIds.LimitPhysics); } }

        public bool hasLimitPhysics { get { return HasComponent(ComponentIds.LimitPhysics); } }

        static readonly Stack<LimitPhysicsComponent> _limitPhysicsComponentPool = new Stack<LimitPhysicsComponent>();

        public static void ClearLimitPhysicsComponentPool() {
            _limitPhysicsComponentPool.Clear();
        }

        public Entity AddLimitPhysics(LimitedAxes newPosition, LimitedAxes newRotation) {
            var component = _limitPhysicsComponentPool.Count > 0 ? _limitPhysicsComponentPool.Pop() : new LimitPhysicsComponent();
            component.Position = newPosition;
            component.Rotation = newRotation;
            return AddComponent(ComponentIds.LimitPhysics, component);
        }

        public Entity ReplaceLimitPhysics(LimitedAxes newPosition, LimitedAxes newRotation) {
            var previousComponent = hasLimitPhysics ? limitPhysics : null;
            var component = _limitPhysicsComponentPool.Count > 0 ? _limitPhysicsComponentPool.Pop() : new LimitPhysicsComponent();
            component.Position = newPosition;
            component.Rotation = newRotation;
            ReplaceComponent(ComponentIds.LimitPhysics, component);
            if (previousComponent != null) {
                _limitPhysicsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLimitPhysics() {
            var component = limitPhysics;
            RemoveComponent(ComponentIds.LimitPhysics);
            _limitPhysicsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLimitPhysics;

        public static AllOfMatcher LimitPhysics {
            get {
                if (_matcherLimitPhysics == null) {
                    _matcherLimitPhysics = new Matcher(ComponentIds.LimitPhysics);
                }

                return _matcherLimitPhysics;
            }
        }
    }
}
