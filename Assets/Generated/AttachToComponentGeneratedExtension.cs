using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AttachToComponent attachTo { get { return (AttachToComponent)GetComponent(ComponentIds.AttachTo); } }

        public bool hasAttachTo { get { return HasComponent(ComponentIds.AttachTo); } }

        static readonly Stack<AttachToComponent> _attachToComponentPool = new Stack<AttachToComponent>();

        public static void ClearAttachToComponentPool() {
            _attachToComponentPool.Clear();
        }

        public Entity AddAttachTo(Entitas.Entity newEntity) {
            var component = _attachToComponentPool.Count > 0 ? _attachToComponentPool.Pop() : new AttachToComponent();
            component.Entity = newEntity;
            return AddComponent(ComponentIds.AttachTo, component);
        }

        public Entity ReplaceAttachTo(Entitas.Entity newEntity) {
            var previousComponent = hasAttachTo ? attachTo : null;
            var component = _attachToComponentPool.Count > 0 ? _attachToComponentPool.Pop() : new AttachToComponent();
            component.Entity = newEntity;
            ReplaceComponent(ComponentIds.AttachTo, component);
            if (previousComponent != null) {
                _attachToComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAttachTo() {
            var component = attachTo;
            RemoveComponent(ComponentIds.AttachTo);
            _attachToComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherAttachTo;

        public static IMatcher AttachTo {
            get {
                if (_matcherAttachTo == null) {
                    _matcherAttachTo = Matcher.AllOf(ComponentIds.AttachTo);
                }

                return _matcherAttachTo;
            }
        }
    }
}
