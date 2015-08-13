namespace Entitas {
    public partial class Pool {
        public ISystem CreateSquadInteractionSystem() {
            return this.CreateSystem<SquadInteractionSystem>();
        }
    }
}