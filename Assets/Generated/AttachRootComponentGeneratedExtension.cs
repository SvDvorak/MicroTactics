using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AttachRootComponent attachRoot { get { return (AttachRootComponent)GetComponent(ComponentIds.AttachRoot); } }

        public bool hasAttachRoot { get { return HasComponent(ComponentIds.AttachRoot); } }

        static readonly Stack<AttachRootComponent> _attachRootComponentPool = new Stack<AttachRootComponent>();

        public static void ClearAttachRootComponentPool() {
            _attachRootComponentPool.Clear();
        }

        public Entity AddAttachRoot(UnityEngine.GameObject newRoot) {
            var component = _attachRootComponentPool.Count > 0 ? _attachRootComponentPool.Pop() : new AttachRootComponent();
            component.Root = newRoot;
            return AddComponent(ComponentIds.AttachRoot, component);
        }

        public Entity ReplaceAttachRoot(UnityEngine.GameObject newRoot) {
            var previousComponent = hasAttachRoot ? attachRoot : null;
            var component = _attachRootComponentPool.Count > 0 ? _attachRootComponentPool.Pop() : new AttachRootComponent();
            component.Root = newRoot;
            ReplaceComponent(ComponentIds.AttachRoot, component);
            if (previousComponent != null) {
                _attachRootComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAttachRoot() {
            var component = attachRoot;
            RemoveComponent(ComponentIds.AttachRoot);
            _attachRootComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAttachRoot;

        public static AllOfMatcher AttachRoot {
            get {
                if (_matcherAttachRoot == null) {
                    _matcherAttachRoot = new Matcher(ComponentIds.AttachRoot);
                }

                return _matcherAttachRoot;
            }
        }
    }
}
