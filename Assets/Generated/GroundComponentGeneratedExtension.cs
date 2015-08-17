namespace Entitas {
    public partial class Entity {
        static readonly GroundComponent groundComponent = new GroundComponent();

        public bool isGround {
            get { return HasComponent(ComponentIds.Ground); }
            set {
                if (value != isGround) {
                    if (value) {
                        AddComponent(ComponentIds.Ground, groundComponent);
                    } else {
                        RemoveComponent(ComponentIds.Ground);
                    }
                }
            }
        }

        public Entity IsGround(bool value) {
            isGround = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherGround;

        public static AllOfMatcher Ground {
            get {
                if (_matcherGround == null) {
                    _matcherGround = new Matcher(ComponentIds.Ground);
                }

                return _matcherGround;
            }
        }
    }
}
