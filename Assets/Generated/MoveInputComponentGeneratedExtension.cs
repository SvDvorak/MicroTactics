using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MoveInputComponent moveInput { get { return (MoveInputComponent)GetComponent(ComponentIds.MoveInput); } }

        public bool hasMoveInput { get { return HasComponent(ComponentIds.MoveInput); } }

        static readonly Stack<MoveInputComponent> _moveInputComponentPool = new Stack<MoveInputComponent>();

        public static void ClearMoveInputComponentPool() {
            _moveInputComponentPool.Clear();
        }

        public Entity AddMoveInput(Mono.GameMath.Vector3 newStart, Mono.GameMath.Vector3 newTarget) {
            var component = _moveInputComponentPool.Count > 0 ? _moveInputComponentPool.Pop() : new MoveInputComponent();
            component.Start = newStart;
            component.Target = newTarget;
            return AddComponent(ComponentIds.MoveInput, component);
        }

        public Entity ReplaceMoveInput(Mono.GameMath.Vector3 newStart, Mono.GameMath.Vector3 newTarget) {
            var previousComponent = hasMoveInput ? moveInput : null;
            var component = _moveInputComponentPool.Count > 0 ? _moveInputComponentPool.Pop() : new MoveInputComponent();
            component.Start = newStart;
            component.Target = newTarget;
            ReplaceComponent(ComponentIds.MoveInput, component);
            if (previousComponent != null) {
                _moveInputComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMoveInput() {
            var component = moveInput;
            RemoveComponent(ComponentIds.MoveInput);
            _moveInputComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMoveInput;

        public static AllOfMatcher MoveInput {
            get {
                if (_matcherMoveInput == null) {
                    _matcherMoveInput = new Matcher(ComponentIds.MoveInput);
                }

                return _matcherMoveInput;
            }
        }
    }
}
