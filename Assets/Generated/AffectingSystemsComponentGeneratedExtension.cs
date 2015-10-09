using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AffectingSystemsComponent affectingSystems { get { return (AffectingSystemsComponent)GetComponent(ComponentIds.AffectingSystems); } }

        public bool hasAffectingSystems { get { return HasComponent(ComponentIds.AffectingSystems); } }

        static readonly Stack<AffectingSystemsComponent> _affectingSystemsComponentPool = new Stack<AffectingSystemsComponent>();

        public static void ClearAffectingSystemsComponentPool() {
            _affectingSystemsComponentPool.Clear();
        }

        public Entity AddAffectingSystems(System.Collections.Generic.List<string> newSystems) {
            var component = _affectingSystemsComponentPool.Count > 0 ? _affectingSystemsComponentPool.Pop() : new AffectingSystemsComponent();
            component.Systems = newSystems;
            return AddComponent(ComponentIds.AffectingSystems, component);
        }

        public Entity ReplaceAffectingSystems(System.Collections.Generic.List<string> newSystems) {
            var previousComponent = hasAffectingSystems ? affectingSystems : null;
            var component = _affectingSystemsComponentPool.Count > 0 ? _affectingSystemsComponentPool.Pop() : new AffectingSystemsComponent();
            component.Systems = newSystems;
            ReplaceComponent(ComponentIds.AffectingSystems, component);
            if (previousComponent != null) {
                _affectingSystemsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAffectingSystems() {
            var component = affectingSystems;
            RemoveComponent(ComponentIds.AffectingSystems);
            _affectingSystemsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAffectingSystems;

        public static AllOfMatcher AffectingSystems {
            get {
                if (_matcherAffectingSystems == null) {
                    _matcherAffectingSystems = new Matcher(ComponentIds.AffectingSystems);
                }

                return _matcherAffectingSystems;
            }
        }
    }
}
