using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public SquadComponent squad { get { return (SquadComponent)GetComponent(ComponentIds.Squad); } }

        public bool hasSquad { get { return HasComponent(ComponentIds.Squad); } }

        static readonly Stack<SquadComponent> _squadComponentPool = new Stack<SquadComponent>();

        public static void ClearSquadComponentPool() {
            _squadComponentPool.Clear();
        }

        public Entity AddSquad(int newNumber) {
            var component = _squadComponentPool.Count > 0 ? _squadComponentPool.Pop() : new SquadComponent();
            component.Number = newNumber;
            return AddComponent(ComponentIds.Squad, component);
        }

        public Entity ReplaceSquad(int newNumber) {
            var previousComponent = hasSquad ? squad : null;
            var component = _squadComponentPool.Count > 0 ? _squadComponentPool.Pop() : new SquadComponent();
            component.Number = newNumber;
            ReplaceComponent(ComponentIds.Squad, component);
            if (previousComponent != null) {
                _squadComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSquad() {
            var component = squad;
            RemoveComponent(ComponentIds.Squad);
            _squadComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSquad;

        public static IMatcher Squad {
            get {
                if (_matcherSquad == null) {
                    _matcherSquad = Matcher.AllOf(ComponentIds.Squad);
                }

                return _matcherSquad;
            }
        }
    }
}
