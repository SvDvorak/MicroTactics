using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ReloadComponent reload { get { return (ReloadComponent)GetComponent(ComponentIds.Reload); } }

        public bool hasReload { get { return HasComponent(ComponentIds.Reload); } }

        static readonly Stack<ReloadComponent> _reloadComponentPool = new Stack<ReloadComponent>();

        public static void ClearReloadComponentPool() {
            _reloadComponentPool.Clear();
        }

        public Entity AddReload(int newFramesLeft) {
            var component = _reloadComponentPool.Count > 0 ? _reloadComponentPool.Pop() : new ReloadComponent();
            component.FramesLeft = newFramesLeft;
            return AddComponent(ComponentIds.Reload, component);
        }

        public Entity ReplaceReload(int newFramesLeft) {
            var previousComponent = hasReload ? reload : null;
            var component = _reloadComponentPool.Count > 0 ? _reloadComponentPool.Pop() : new ReloadComponent();
            component.FramesLeft = newFramesLeft;
            ReplaceComponent(ComponentIds.Reload, component);
            if (previousComponent != null) {
                _reloadComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveReload() {
            var component = reload;
            RemoveComponent(ComponentIds.Reload);
            _reloadComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherReload;

        public static IMatcher Reload {
            get {
                if (_matcherReload == null) {
                    _matcherReload = Matcher.AllOf(ComponentIds.Reload);
                }

                return _matcherReload;
            }
        }
    }
}
