namespace Entitas {
    public partial class Entity {
        static readonly PitchFromVelocityComponent pitchFromVelocityComponent = new PitchFromVelocityComponent();

        public bool isPitchFromVelocity {
            get { return HasComponent(ComponentIds.PitchFromVelocity); }
            set {
                if (value != isPitchFromVelocity) {
                    if (value) {
                        AddComponent(ComponentIds.PitchFromVelocity, pitchFromVelocityComponent);
                    } else {
                        RemoveComponent(ComponentIds.PitchFromVelocity);
                    }
                }
            }
        }

        public Entity IsPitchFromVelocity(bool value) {
            isPitchFromVelocity = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherPitchFromVelocity;

        public static AllOfMatcher PitchFromVelocity {
            get {
                if (_matcherPitchFromVelocity == null) {
                    _matcherPitchFromVelocity = new Matcher(ComponentIds.PitchFromVelocity);
                }

                return _matcherPitchFromVelocity;
            }
        }
    }
}
