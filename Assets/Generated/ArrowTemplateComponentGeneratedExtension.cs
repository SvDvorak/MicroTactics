using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ArrowTemplateComponent arrowTemplate { get { return (ArrowTemplateComponent)GetComponent(ComponentIds.ArrowTemplate); } }

        public bool hasArrowTemplate { get { return HasComponent(ComponentIds.ArrowTemplate); } }

        static readonly Stack<ArrowTemplateComponent> _arrowTemplateComponentPool = new Stack<ArrowTemplateComponent>();

        public static void ClearArrowTemplateComponentPool() {
            _arrowTemplateComponentPool.Clear();
        }

        public Entity AddArrowTemplate(UnityEngine.GameObject newTemplate) {
            var component = _arrowTemplateComponentPool.Count > 0 ? _arrowTemplateComponentPool.Pop() : new ArrowTemplateComponent();
            component.Template = newTemplate;
            return AddComponent(ComponentIds.ArrowTemplate, component);
        }

        public Entity ReplaceArrowTemplate(UnityEngine.GameObject newTemplate) {
            var previousComponent = hasArrowTemplate ? arrowTemplate : null;
            var component = _arrowTemplateComponentPool.Count > 0 ? _arrowTemplateComponentPool.Pop() : new ArrowTemplateComponent();
            component.Template = newTemplate;
            ReplaceComponent(ComponentIds.ArrowTemplate, component);
            if (previousComponent != null) {
                _arrowTemplateComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveArrowTemplate() {
            var component = arrowTemplate;
            RemoveComponent(ComponentIds.ArrowTemplate);
            _arrowTemplateComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherArrowTemplate;

        public static AllOfMatcher ArrowTemplate {
            get {
                if (_matcherArrowTemplate == null) {
                    _matcherArrowTemplate = new Matcher(ComponentIds.ArrowTemplate);
                }

                return _matcherArrowTemplate;
            }
        }
    }
}
