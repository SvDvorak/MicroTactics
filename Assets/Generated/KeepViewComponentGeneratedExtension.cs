namespace Entitas {
    public partial class Entity {
        static readonly KeepViewComponent keepViewComponent = new KeepViewComponent();

        public bool isKeepView {
            get { return HasComponent(ComponentIds.KeepView); }
            set {
                if (value != isKeepView) {
                    if (value) {
                        AddComponent(ComponentIds.KeepView, keepViewComponent);
                    } else {
                        RemoveComponent(ComponentIds.KeepView);
                    }
                }
            }
        }

        public Entity IsKeepView(bool value) {
            isKeepView = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherKeepView;

        public static AllOfMatcher KeepView {
            get {
                if (_matcherKeepView == null) {
                    _matcherKeepView = new Matcher(ComponentIds.KeepView);
                }

                return _matcherKeepView;
            }
        }
    }
}
