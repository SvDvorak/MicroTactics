using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public UnitTemplateComponent unitTemplate { get { return (UnitTemplateComponent)GetComponent(ComponentIds.UnitTemplate); } }

        public bool hasUnitTemplate { get { return HasComponent(ComponentIds.UnitTemplate); } }

        static readonly Stack<UnitTemplateComponent> _unitTemplateComponentPool = new Stack<UnitTemplateComponent>();

        public static void ClearUnitTemplateComponentPool() {
            _unitTemplateComponentPool.Clear();
        }

        public Entity AddUnitTemplate(UnityEngine.GameObject newTemplate) {
            var component = _unitTemplateComponentPool.Count > 0 ? _unitTemplateComponentPool.Pop() : new UnitTemplateComponent();
            component.Template = newTemplate;
            return AddComponent(ComponentIds.UnitTemplate, component);
        }

        public Entity ReplaceUnitTemplate(UnityEngine.GameObject newTemplate) {
            var previousComponent = hasUnitTemplate ? unitTemplate : null;
            var component = _unitTemplateComponentPool.Count > 0 ? _unitTemplateComponentPool.Pop() : new UnitTemplateComponent();
            component.Template = newTemplate;
            ReplaceComponent(ComponentIds.UnitTemplate, component);
            if (previousComponent != null) {
                _unitTemplateComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveUnitTemplate() {
            var component = unitTemplate;
            RemoveComponent(ComponentIds.UnitTemplate);
            _unitTemplateComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherUnitTemplate;

        public static AllOfMatcher UnitTemplate {
            get {
                if (_matcherUnitTemplate == null) {
                    _matcherUnitTemplate = new Matcher(ComponentIds.UnitTemplate);
                }

                return _matcherUnitTemplate;
            }
        }
    }
}
