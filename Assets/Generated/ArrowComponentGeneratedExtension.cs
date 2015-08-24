namespace Entitas {
    public partial class Entity {
        static readonly ArrowComponent arrowComponent = new ArrowComponent();

        public bool isArrow {
            get { return HasComponent(ComponentIds.Arrow); }
            set {
                if (value != isArrow) {
                    if (value) {
                        AddComponent(ComponentIds.Arrow, arrowComponent);
                    } else {
                        RemoveComponent(ComponentIds.Arrow);
                    }
                }
            }
        }

        public Entity IsArrow(bool value) {
            isArrow = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherArrow;

        public static AllOfMatcher Arrow {
            get {
                if (_matcherArrow == null) {
                    _matcherArrow = new Matcher(ComponentIds.Arrow);
                }

                return _matcherArrow;
            }
        }
    }
}
