namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectSquadSystem() {
            return this.CreateSystem<SquadInteractionSystem>();
        }
    }
}