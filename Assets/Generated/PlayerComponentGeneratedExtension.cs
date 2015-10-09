namespace Entitas {
    public partial class Entity {
        static readonly PlayerComponent playerComponent = new PlayerComponent();

        public bool isPlayer {
            get { return HasComponent(ComponentIds.Player); }
            set {
                if (value != isPlayer) {
                    if (value) {
                        AddComponent(ComponentIds.Player, playerComponent);
                    } else {
                        RemoveComponent(ComponentIds.Player);
                    }
                }
            }
        }

        public Entity IsPlayer(bool value) {
            isPlayer = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayer;

        public static IMatcher Player {
            get {
                if (_matcherPlayer == null) {
                    _matcherPlayer = Matcher.AllOf(ComponentIds.Player);
                }

                return _matcherPlayer;
            }
        }
    }
}
