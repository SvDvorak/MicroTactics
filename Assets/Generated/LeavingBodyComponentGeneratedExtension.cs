namespace Entitas {
    public partial class Entity {
        static readonly LeavingBodyComponent leavingBodyComponent = new LeavingBodyComponent();

        public bool isLeavingBody {
            get { return HasComponent(ComponentIds.LeavingBody); }
            set {
                if (value != isLeavingBody) {
                    if (value) {
                        AddComponent(ComponentIds.LeavingBody, leavingBodyComponent);
                    } else {
                        RemoveComponent(ComponentIds.LeavingBody);
                    }
                }
            }
        }

        public Entity IsLeavingBody(bool value) {
            isLeavingBody = value;
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherLeavingBody;

        public static AllOfMatcher LeavingBody {
            get {
                if (_matcherLeavingBody == null) {
                    _matcherLeavingBody = new Matcher(ComponentIds.LeavingBody);
                }

                return _matcherLeavingBody;
            }
        }
    }
}
