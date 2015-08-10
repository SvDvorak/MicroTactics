using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public AnimateComponent animate { get { return (AnimateComponent)GetComponent(ComponentIds.Animate); } }

        public bool hasAnimate { get { return HasComponent(ComponentIds.Animate); } }

        static readonly Stack<AnimateComponent> _animateComponentPool = new Stack<AnimateComponent>();

        public static void ClearAnimateComponentPool() {
            _animateComponentPool.Clear();
        }

        public Entity AddAnimate(UnityEngine.Animator newAnimator) {
            var component = _animateComponentPool.Count > 0 ? _animateComponentPool.Pop() : new AnimateComponent();
            component.Animator = newAnimator;
            return AddComponent(ComponentIds.Animate, component);
        }

        public Entity ReplaceAnimate(UnityEngine.Animator newAnimator) {
            var previousComponent = hasAnimate ? animate : null;
            var component = _animateComponentPool.Count > 0 ? _animateComponentPool.Pop() : new AnimateComponent();
            component.Animator = newAnimator;
            ReplaceComponent(ComponentIds.Animate, component);
            if (previousComponent != null) {
                _animateComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveAnimate() {
            var component = animate;
            RemoveComponent(ComponentIds.Animate);
            _animateComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherAnimate;

        public static AllOfMatcher Animate {
            get {
                if (_matcherAnimate == null) {
                    _matcherAnimate = new Matcher(ComponentIds.Animate);
                }

                return _matcherAnimate;
            }
        }
    }
}
