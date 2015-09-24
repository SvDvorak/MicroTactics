namespace Entitas {
    public partial class Pool {
        public ISystem CreateLimitPhysicsSystem() {
            return this.CreateSystem<Assets.Features.LimitPhysicsSystem>();
        }
    }
}