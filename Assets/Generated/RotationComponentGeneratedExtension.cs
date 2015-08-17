using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RotationComponent rotation { get { return (RotationComponent)GetComponent(ComponentIds.Rotation); } }

        public bool hasRotation { get { return HasComponent(ComponentIds.Rotation); } }

        static readonly Stack<RotationComponent> _rotationComponentPool = new Stack<RotationComponent>();

        public static void ClearRotationComponentPool() {
            _rotationComponentPool.Clear();
        }

        public Entity AddRotation(float newX, float newY, float newZ, float newW) {
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new RotationComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            component.w = newW;
            return AddComponent(ComponentIds.Rotation, component);
        }

        public Entity ReplaceRotation(float newX, float newY, float newZ, float newW) {
            var previousComponent = hasRotation ? rotation : null;
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new RotationComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            component.w = newW;
            ReplaceComponent(ComponentIds.Rotation, component);
            if (previousComponent != null) {
                _rotationComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRotation() {
            var component = rotation;
            RemoveComponent(ComponentIds.Rotation);
            _rotationComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherRotation;

        public static AllOfMatcher Rotation {
            get {
                if (_matcherRotation == null) {
                    _matcherRotation = new Matcher(ComponentIds.Rotation);
                }

                return _matcherRotation;
            }
        }
    }
}
