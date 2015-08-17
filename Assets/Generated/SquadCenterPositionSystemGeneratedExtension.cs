namespace Entitas {
    public partial class Pool {
        public ISystem CreateSquadCenterPositionSystem() {
            return this.CreateSystem<SquadCenterPositionSystem>();
        }
    }
}