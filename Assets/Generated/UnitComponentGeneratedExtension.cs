using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public UnitComponent unit { get { return (UnitComponent)GetComponent(ComponentIds.Unit); } }

        public bool hasUnit { get { return HasComponent(ComponentIds.Unit); } }

        static readonly Stack<UnitComponent> _unitComponentPool = new Stack<UnitComponent>();

        public static void ClearUnitComponentPool() {
            _unitComponentPool.Clear();
        }

        public Entity AddUnit(int newSquadNumber) {
            var component = _unitComponentPool.Count > 0 ? _unitComponentPool.Pop() : new UnitComponent();
            component.SquadNumber = newSquadNumber;
            return AddComponent(ComponentIds.Unit, component);
        }

        public Entity ReplaceUnit(int newSquadNumber) {
            var previousComponent = hasUnit ? unit : null;
            var component = _unitComponentPool.Count > 0 ? _unitComponentPool.Pop() : new UnitComponent();
            component.SquadNumber = newSquadNumber;
            ReplaceComponent(ComponentIds.Unit, component);
            if (previousComponent != null) {
                _unitComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveUnit() {
            var component = unit;
            RemoveComponent(ComponentIds.Unit);
            _unitComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherUnit;

        public static IMatcher Unit {
            get {
                if (_matcherUnit == null) {
                    _matcherUnit = Matcher.AllOf(ComponentIds.Unit);
                }

                return _matcherUnit;
            }
        }
    }
}
