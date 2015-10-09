namespace Entitas {
    public partial class Pool {
        public ISystem CreateCreateSquadSystem() {
            return this.CreateSystem<CreateSquadSystem>();
        }
    }
}