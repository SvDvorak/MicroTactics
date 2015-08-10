using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputPressComponent inputPress { get { return (InputPressComponent)GetComponent(ComponentIds.InputPress); } }

        public bool hasInputPress { get { return HasComponent(ComponentIds.InputPress); } }

        static readonly Stack<InputPressComponent> _inputPressComponentPool = new Stack<InputPressComponent>();

        public static void ClearInputPressComponentPool() {
            _inputPressComponentPool.Clear();
        }

        public Entity AddInputPress(System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var component = _inputPressComponentPool.Count > 0 ? _inputPressComponentPool.Pop() : new InputPressComponent();
            component.Entities = newEntities;
            return AddComponent(ComponentIds.InputPress, component);
        }

        public Entity ReplaceInputPress(System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var previousComponent = hasInputPress ? inputPress : null;
            var component = _inputPressComponentPool.Count > 0 ? _inputPressComponentPool.Pop() : new InputPressComponent();
            component.Entities = newEntities;
            ReplaceComponent(ComponentIds.InputPress, component);
            if (previousComponent != null) {
                _inputPressComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputPress() {
            var component = inputPress;
            RemoveComponent(ComponentIds.InputPress);
            _inputPressComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherInputPress;

        public static AllOfMatcher InputPress {
            get {
                if (_matcherInputPress == null) {
                    _matcherInputPress = new Matcher(ComponentIds.InputPress);
                }

                return _matcherInputPress;
            }
        }
    }
}
