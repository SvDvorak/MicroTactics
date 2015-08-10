using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ArrowComponent arrow { get { return (ArrowComponent)GetComponent(ComponentIds.Arrow); } }

        public bool hasArrow { get { return HasComponent(ComponentIds.Arrow); } }

        static readonly Stack<ArrowComponent> _arrowComponentPool = new Stack<ArrowComponent>();

        public static void ClearArrowComponentPool() {
            _arrowComponentPool.Clear();
        }

        public Entity AddArrow(Mono.GameMath.Vector3 newPosition, Mono.GameMath.Quaternion newRotation, Mono.GameMath.Vector3 newForce) {
            var component = _arrowComponentPool.Count > 0 ? _arrowComponentPool.Pop() : new ArrowComponent();
            component.Position = newPosition;
            component.Rotation = newRotation;
            component.Force = newForce;
            return AddComponent(ComponentIds.Arrow, component);
        }

        public Entity ReplaceArrow(Mono.GameMath.Vector3 newPosition, Mono.GameMath.Quaternion newRotation, Mono.GameMath.Vector3 newForce) {
            var previousComponent = hasArrow ? arrow : null;
            var component = _arrowComponentPool.Count > 0 ? _arrowComponentPool.Pop() : new ArrowComponent();
            component.Position = newPosition;
            component.Rotation = newRotation;
            component.Force = newForce;
            ReplaceComponent(ComponentIds.Arrow, component);
            if (previousComponent != null) {
                _arrowComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveArrow() {
            var component = arrow;
            RemoveComponent(ComponentIds.Arrow);
            _arrowComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherArrow;

        public static AllOfMatcher Arrow {
            get {
                if (_matcherArrow == null) {
                    _matcherArrow = new Matcher(ComponentIds.Arrow);
                }

                return _matcherArrow;
            }
        }
    }
}
