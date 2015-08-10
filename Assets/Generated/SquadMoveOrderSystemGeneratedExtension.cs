namespace Entitas {
    public partial class Pool {
        public ISystem CreateSquadMoveOrderSystem() {
            return this.CreateSystem<SquadMoveOrderSystem>();
        }
    }
}