using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MoveOrderComponent moveOrder { get { return (MoveOrderComponent)GetComponent(ComponentIds.MoveOrder); } }

        public bool hasMoveOrder { get { return HasComponent(ComponentIds.MoveOrder); } }

        static readonly Stack<MoveOrderComponent> _moveOrderComponentPool = new Stack<MoveOrderComponent>();

        public static void ClearMoveOrderComponentPool() {
            _moveOrderComponentPool.Clear();
        }

        public Entity AddMoveOrder(float newX, float newY, float newZ) {
            var component = _moveOrderComponentPool.Count > 0 ? _moveOrderComponentPool.Pop() : new MoveOrderComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            return AddComponent(ComponentIds.MoveOrder, component);
        }

        public Entity ReplaceMoveOrder(float newX, float newY, float newZ) {
            var previousComponent = hasMoveOrder ? moveOrder : null;
            var component = _moveOrderComponentPool.Count > 0 ? _moveOrderComponentPool.Pop() : new MoveOrderComponent();
            component.x = newX;
            component.y = newY;
            component.z = newZ;
            ReplaceComponent(ComponentIds.MoveOrder, component);
            if (previousComponent != null) {
                _moveOrderComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMoveOrder() {
            var component = moveOrder;
            RemoveComponent(ComponentIds.MoveOrder);
            _moveOrderComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherMoveOrder;

        public static AllOfMatcher MoveOrder {
            get {
                if (_matcherMoveOrder == null) {
                    _matcherMoveOrder = new Matcher(ComponentIds.MoveOrder);
                }

                return _matcherMoveOrder;
            }
        }
    }
}
