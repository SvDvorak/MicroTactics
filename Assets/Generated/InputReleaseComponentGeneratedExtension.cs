using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputReleaseComponent inputRelease { get { return (InputReleaseComponent)GetComponent(ComponentIds.InputRelease); } }

        public bool hasInputRelease { get { return HasComponent(ComponentIds.InputRelease); } }

        static readonly Stack<InputReleaseComponent> _inputReleaseComponentPool = new Stack<InputReleaseComponent>();

        public static void ClearInputReleaseComponentPool() {
            _inputReleaseComponentPool.Clear();
        }

        public Entity AddInputRelease(System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var component = _inputReleaseComponentPool.Count > 0 ? _inputReleaseComponentPool.Pop() : new InputReleaseComponent();
            component.Entities = newEntities;
            return AddComponent(ComponentIds.InputRelease, component);
        }

        public Entity ReplaceInputRelease(System.Collections.Generic.List<Entitas.Entity> newEntities) {
            var previousComponent = hasInputRelease ? inputRelease : null;
            var component = _inputReleaseComponentPool.Count > 0 ? _inputReleaseComponentPool.Pop() : new InputReleaseComponent();
            component.Entities = newEntities;
            ReplaceComponent(ComponentIds.InputRelease, component);
            if (previousComponent != null) {
                _inputReleaseComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputRelease() {
            var component = inputRelease;
            RemoveComponent(ComponentIds.InputRelease);
            _inputReleaseComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherInputRelease;

        public static AllOfMatcher InputRelease {
            get {
                if (_matcherInputRelease == null) {
                    _matcherInputRelease = new Matcher(ComponentIds.InputRelease);
                }

                return _matcherInputRelease;
            }
        }
    }
}
