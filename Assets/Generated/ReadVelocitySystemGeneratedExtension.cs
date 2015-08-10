namespace Entitas {
    public partial class Pool {
        public ISystem CreateReadVelocitySystem() {
            return this.CreateSystem<ReadVelocitySystem>();
        }
    }
}