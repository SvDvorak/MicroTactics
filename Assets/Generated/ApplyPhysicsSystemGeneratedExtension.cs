namespace Entitas {
    public partial class Pool {
        public ISystem CreateApplyPhysicsSystem() {
            return this.CreateSystem<ApplyPhysicsSystem>();
        }
    }
}