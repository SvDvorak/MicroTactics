using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public UnitsCacheComponent unitsCache { get { return (UnitsCacheComponent)GetComponent(ComponentIds.UnitsCache); } }

        public bool hasUnitsCache { get { return HasComponent(ComponentIds.UnitsCache); } }

        static readonly Stack<UnitsCacheComponent> _unitsCacheComponentPool = new Stack<UnitsCacheComponent>();

        public static void ClearUnitsCacheComponentPool() {
            _unitsCacheComponentPool.Clear();
        }

        public Entity AddUnitsCache(System.Collections.Generic.List<Entitas.Entity> newUnits) {
            var component = _unitsCacheComponentPool.Count > 0 ? _unitsCacheComponentPool.Pop() : new UnitsCacheComponent();
            component.Units = newUnits;
            return AddComponent(ComponentIds.UnitsCache, component);
        }

        public Entity ReplaceUnitsCache(System.Collections.Generic.List<Entitas.Entity> newUnits) {
            var previousComponent = hasUnitsCache ? unitsCache : null;
            var component = _unitsCacheComponentPool.Count > 0 ? _unitsCacheComponentPool.Pop() : new UnitsCacheComponent();
            component.Units = newUnits;
            ReplaceComponent(ComponentIds.UnitsCache, component);
            if (previousComponent != null) {
                _unitsCacheComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveUnitsCache() {
            var component = unitsCache;
            RemoveComponent(ComponentIds.UnitsCache);
            _unitsCacheComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherUnitsCache;

        public static AllOfMatcher UnitsCache {
            get {
                if (_matcherUnitsCache == null) {
                    _matcherUnitsCache = new Matcher(ComponentIds.UnitsCache);
                }

                return _matcherUnitsCache;
            }
        }
    }
}
