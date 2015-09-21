namespace Entitas {
    public partial class Entity {
        static readonly HiddenComponent hiddenComponent = new HiddenComponent();

        public bool isHidden {
            get { return HasComponent(ComponentIds.Hidden); }
            set {
                if (value != isHidden) {
                    if (value) {
                        AddComponent(ComponentIds.Hidden, hiddenComponent);
                    } else {
                        RemoveComponent(ComponentIds.Hidden);
                    }
                }
            }
        }

        public Entity IsHidden(bool value) {
            isHidden = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherHidden;

        public static AllOfMatcher Hidden {
            get {
                if (_matcherHidden == null) {
                    _matcherHidden = new Matcher(ComponentIds.Hidden);
                }

                return _matcherHidden;
            }
        }
    }
}
