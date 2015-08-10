using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MovementComponent movement { get { return (MovementComponent)GetComponent(ComponentIds.Movement); } }

        public bool hasMovement { get { return HasComponent(ComponentIds.Movement); } }

        static readonly Stack<MovementComponent> _movementComponentPool = new Stack<MovementComponent>();

        public static void ClearMovementComponentPool() {
            _movementComponentPool.Clear();
        }

        public Entity AddMovement(float newMoveSpeed) {
            var component = _movementComponentPool.Count > 0 ? _movementComponentPool.Pop() : new MovementComponent();
            component.MoveSpeed = newMoveSpeed;
            return AddComponent(ComponentIds.Movement, component);
        }

        public Entity ReplaceMovement(float newMoveSpeed) {
            var previousComponent = hasMovement ? movement : null;
            var component = _movementComponentPool.Count > 0 ? _movementComponentPool.Pop() : new MovementComponent();
            component.MoveSpeed = newMoveSpeed;
            ReplaceComponent(ComponentIds.Movement, component);
            if (previousComponent != null) {
                _movementComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMovement() {
            var component = movement;
            RemoveComponent(ComponentIds.Movement);
            _movementComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMovement;

        public static AllOfMatcher Movement {
            get {
                if (_matcherMovement == null) {
                    _matcherMovement = new Matcher(ComponentIds.Movement);
                }

                return _matcherMovement;
            }
        }
    }
}
