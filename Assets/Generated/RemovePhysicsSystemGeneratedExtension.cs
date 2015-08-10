namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemovePhysicsSystem() {
            return this.CreateSystem<RemovePhysicsSystem>();
        }
    }
}