using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputPositionChangedComponent inputPositionChanged { get { return (InputPositionChangedComponent)GetComponent(ComponentIds.InputPositionChanged); } }

        public bool hasInputPositionChanged { get { return HasComponent(ComponentIds.InputPositionChanged); } }

        static readonly Stack<InputPositionChangedComponent> _inputPositionChangedComponentPool = new Stack<InputPositionChangedComponent>();

        public static void ClearInputPositionChangedComponentPool() {
            _inputPositionChangedComponentPool.Clear();
        }

        public Entity AddInputPositionChanged(System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var component = _inputPositionChangedComponentPool.Count > 0 ? _inputPositionChangedComponentPool.Pop() : new InputPositionChangedComponent();
            component.Entities = newEntities;
            return AddComponent(ComponentIds.InputPositionChanged, component);
        }

        public Entity ReplaceInputPositionChanged(System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var previousComponent = hasInputPositionChanged ? inputPositionChanged : null;
            var component = _inputPositionChangedComponentPool.Count > 0 ? _inputPositionChangedComponentPool.Pop() : new InputPositionChangedComponent();
            component.Entities = newEntities;
            ReplaceComponent(ComponentIds.InputPositionChanged, component);
            if (previousComponent != null) {
                _inputPositionChangedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputPositionChanged() {
            var component = inputPositionChanged;
            RemoveComponent(ComponentIds.InputPositionChanged);
            _inputPositionChangedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherInputPositionChanged;

        public static AllOfMatcher InputPositionChanged {
            get {
                if (_matcherInputPositionChanged == null) {
                    _matcherInputPositionChanged = new Matcher(ComponentIds.InputPositionChanged);
                }

                return _matcherInputPositionChanged;
            }
        }
    }
}
