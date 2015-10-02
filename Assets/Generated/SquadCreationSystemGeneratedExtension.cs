namespace Entitas {
    public partial class Pool {
        public ISystem CreateSquadCreationSystem() {
            return this.CreateSystem<CreateSquadSystem>();
        }
    }
}