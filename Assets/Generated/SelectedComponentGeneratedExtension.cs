namespace Entitas {
    public partial class Entity {
        static readonly SelectedComponent selectedComponent = new SelectedComponent();

        public bool isSelected {
            get { return HasComponent(ComponentIds.Selected); }
            set {
                if (value != isSelected) {
                    if (value) {
                        AddComponent(ComponentIds.Selected, selectedComponent);
                    } else {
                        RemoveComponent(ComponentIds.Selected);
                    }
                }
            }
        }

        public Entity IsSelected(bool value) {
            isSelected = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSelected;

        public static IMatcher Selected {
            get {
                if (_matcherSelected == null) {
                    _matcherSelected = Matcher.AllOf(ComponentIds.Selected);
                }

                return _matcherSelected;
            }
        }
    }
}
