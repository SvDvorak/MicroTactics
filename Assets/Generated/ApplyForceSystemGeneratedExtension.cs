namespace Entitas {
    public partial class Pool {
        public ISystem CreateApplyForceSystem() {
            return this.CreateSystem<ApplyForceSystem>();
        }
    }
}