using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AttackOrderComponent attackOrder { get { return (AttackOrderComponent)GetComponent(ComponentIds.AttackOrder); } }

        public bool hasAttackOrder { get { return HasComponent(ComponentIds.AttackOrder); } }

        static readonly Stack<AttackOrderComponent> _attackOrderComponentPool = new Stack<AttackOrderComponent>();

        public static void ClearAttackOrderComponentPool() {
            _attackOrderComponentPool.Clear();
        }

        public Entity AddAttackOrder(float newX, float newY, float newZ) {
            var component = _attackOrderComponentPool.Count > 0 ? _attackOrderComponentPool.Pop() : new AttackOrderComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            return AddComponent(ComponentIds.AttackOrder, component);
        }

        public Entity ReplaceAttackOrder(float newX, float newY, float newZ) {
            var previousComponent = hasAttackOrder ? attackOrder : null;
            var component = _attackOrderComponentPool.Count > 0 ? _attackOrderComponentPool.Pop() : new AttackOrderComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            ReplaceComponent(ComponentIds.AttackOrder, component);
            if (previousComponent != null) {
                _attackOrderComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAttackOrder() {
            var component = attackOrder;
            RemoveComponent(ComponentIds.AttackOrder);
            _attackOrderComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAttackOrder;

        public static AllOfMatcher AttackOrder {
            get {
                if (_matcherAttackOrder == null) {
                    _matcherAttackOrder = new Matcher(ComponentIds.AttackOrder);
                }

                return _matcherAttackOrder;
            }
        }
    }
}
