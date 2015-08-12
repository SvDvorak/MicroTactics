using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputComponent input { get { return (InputComponent)GetComponent(ComponentIds.Input); } }

        public bool hasInput { get { return HasComponent(ComponentIds.Input); } }

        static readonly Stack<InputComponent> _inputComponentPool = new Stack<InputComponent>();

        public static void ClearInputComponentPool() {
            _inputComponentPool.Clear();
        }

        public Entity AddInput(InputState newState, System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var component = _inputComponentPool.Count > 0 ? _inputComponentPool.Pop() : new InputComponent();
            component.State = newState;
            component.Entities = newEntities;
            return AddComponent(ComponentIds.Input, component);
        }

        public Entity ReplaceInput(InputState newState, System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var previousComponent = hasInput ? input : null;
            var component = _inputComponentPool.Count > 0 ? _inputComponentPool.Pop() : new InputComponent();
            component.State = newState;
            component.Entities = newEntities;
            ReplaceComponent(ComponentIds.Input, component);
            if (previousComponent != null) {
                _inputComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInput() {
            var component = input;
            RemoveComponent(ComponentIds.Input);
            _inputComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherInput;

        public static AllOfMatcher Input {
            get {
                if (_matcherInput == null) {
                    _matcherInput = new Matcher(ComponentIds.Input);
                }

                return _matcherInput;
            }
        }
    }
}
