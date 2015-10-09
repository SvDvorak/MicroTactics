using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DelayedDestroyComponent delayedDestroy { get { return (DelayedDestroyComponent)GetComponent(ComponentIds.DelayedDestroy); } }

        public bool hasDelayedDestroy { get { return HasComponent(ComponentIds.DelayedDestroy); } }

        static readonly Stack<DelayedDestroyComponent> _delayedDestroyComponentPool = new Stack<DelayedDestroyComponent>();

        public static void ClearDelayedDestroyComponentPool() {
            _delayedDestroyComponentPool.Clear();
        }

        public Entity AddDelayedDestroy(int newFrames) {
            var component = _delayedDestroyComponentPool.Count > 0 ? _delayedDestroyComponentPool.Pop() : new DelayedDestroyComponent();
            component.Frames = newFrames;
            return AddComponent(ComponentIds.DelayedDestroy, component);
        }

        public Entity ReplaceDelayedDestroy(int newFrames) {
            var previousComponent = hasDelayedDestroy ? delayedDestroy : null;
            var component = _delayedDestroyComponentPool.Count > 0 ? _delayedDestroyComponentPool.Pop() : new DelayedDestroyComponent();
            component.Frames = newFrames;
            ReplaceComponent(ComponentIds.DelayedDestroy, component);
            if (previousComponent != null) {
                _delayedDestroyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDelayedDestroy() {
            var component = delayedDestroy;
            RemoveComponent(ComponentIds.DelayedDestroy);
            _delayedDestroyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDelayedDestroy;

        public static IMatcher DelayedDestroy {
            get {
                if (_matcherDelayedDestroy == null) {
                    _matcherDelayedDestroy = Matcher.AllOf(ComponentIds.DelayedDestroy);
                }

                return _matcherDelayedDestroy;
            }
        }
    }
}
