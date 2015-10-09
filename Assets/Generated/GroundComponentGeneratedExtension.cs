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
        static IMatcher _matcherGround;

        public static IMatcher Ground {
            get {
                if (_matcherGround == null) {
                    _matcherGround = Matcher.AllOf(ComponentIds.Ground);
                }

                return _matcherGround;
            }
        }
    }
}
