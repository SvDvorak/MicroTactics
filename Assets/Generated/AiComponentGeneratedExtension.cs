using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AiComponent ai { get { return (AiComponent)GetComponent(ComponentIds.Ai); } }

        public bool hasAi { get { return HasComponent(ComponentIds.Ai); } }

        static readonly Stack<AiComponent> _aiComponentPool = new Stack<AiComponent>();

        public static void ClearAiComponentPool() {
            _aiComponentPool.Clear();
        }

        public Entity AddAi(float newSeeingRange) {
            var component = _aiComponentPool.Count > 0 ? _aiComponentPool.Pop() : new AiComponent();
            component.SeeingRange = newSeeingRange;
            return AddComponent(ComponentIds.Ai, component);
        }

        public Entity ReplaceAi(float newSeeingRange) {
            var previousComponent = hasAi ? ai : null;
            var component = _aiComponentPool.Count > 0 ? _aiComponentPool.Pop() : new AiComponent();
            component.SeeingRange = newSeeingRange;
            ReplaceComponent(ComponentIds.Ai, component);
            if (previousComponent != null) {
                _aiComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAi() {
            var component = ai;
            RemoveComponent(ComponentIds.Ai);
            _aiComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAi;

        public static AllOfMatcher Ai {
            get {
                if (_matcherAi == null) {
                    _matcherAi = new Matcher(ComponentIds.Ai);
                }

                return _matcherAi;
            }
        }
    }
}
