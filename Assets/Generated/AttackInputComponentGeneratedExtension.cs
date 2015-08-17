using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AttackInputComponent attackInput { get { return (AttackInputComponent)GetComponent(ComponentIds.AttackInput); } }

        public bool hasAttackInput { get { return HasComponent(ComponentIds.AttackInput); } }

        static readonly Stack<AttackInputComponent> _attackInputComponentPool = new Stack<AttackInputComponent>();

        public static void ClearAttackInputComponentPool() {
            _attackInputComponentPool.Clear();
        }

        public Entity AddAttackInput(Mono.GameMath.Vector3 newStart, Mono.GameMath.Vector3 newTarget) {
            var component = _attackInputComponentPool.Count > 0 ? _attackInputComponentPool.Pop() : new AttackInputComponent();
            component.Start = newStart;
            component.Target = newTarget;
            return AddComponent(ComponentIds.AttackInput, component);
        }

        public Entity ReplaceAttackInput(Mono.GameMath.Vector3 newStart, Mono.GameMath.Vector3 newTarget) {
            var previousComponent = hasAttackInput ? attackInput : null;
            var component = _attackInputComponentPool.Count > 0 ? _attackInputComponentPool.Pop() : new AttackInputComponent();
            component.Start = newStart;
            component.Target = newTarget;
            ReplaceComponent(ComponentIds.AttackInput, component);
            if (previousComponent != null) {
                _attackInputComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAttackInput() {
            var component = attackInput;
            RemoveComponent(ComponentIds.AttackInput);
            _attackInputComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAttackInput;

        public static AllOfMatcher AttackInput {
            get {
                if (_matcherAttackInput == null) {
                    _matcherAttackInput = new Matcher(ComponentIds.AttackInput);
                }

                return _matcherAttackInput;
            }
        }
    }
}
