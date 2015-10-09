namespace Entitas {
    public partial class Pool {
        public ISystem CreateReadPhysicsSystem() {
            return this.CreateSystem<ReadPhysicsSystem>();
        }
    }
}