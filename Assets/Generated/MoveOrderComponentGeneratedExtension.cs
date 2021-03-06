using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MoveOrderComponent moveOrder { get { return (MoveOrderComponent)GetComponent(ComponentIds.MoveOrder); } }

        public bool hasMoveOrder { get { return HasComponent(ComponentIds.MoveOrder); } }

        static readonly Stack<MoveOrderComponent> _moveOrderComponentPool = new Stack<MoveOrderComponent>();

        public static void ClearMoveOrderComponentPool() {
            _moveOrderComponentPool.Clear();
        }

        public Entity AddMoveOrder(Mono.GameMath.Vector3 newPosition, Mono.GameMath.Quaternion newOrientation) {
            var component = _moveOrderComponentPool.Count > 0 ? _moveOrderComponentPool.Pop() : new MoveOrderComponent();
            component.Position = newPosition;
            component.Orientation = newOrientation;
            return AddComponent(ComponentIds.MoveOrder, component);
        }

        public Entity ReplaceMoveOrder(Mono.GameMath.Vector3 newPosition, Mono.GameMath.Quaternion newOrientation) {
            var previousComponent = hasMoveOrder ? moveOrder : null;
            var component = _moveOrderComponentPool.Count > 0 ? _moveOrderComponentPool.Pop() : new MoveOrderComponent();
            component.Position = newPosition;
            component.Orientation = newOrientation;
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
        static IMatcher _matcherMoveOrder;

        public static IMatcher MoveOrder {
            get {
                if (_matcherMoveOrder == null) {
                    _matcherMoveOrder = Matcher.AllOf(ComponentIds.MoveOrder);
                }

                return _matcherMoveOrder;
            }
        }
    }
}
