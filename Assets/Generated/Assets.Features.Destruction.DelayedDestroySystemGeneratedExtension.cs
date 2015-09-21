namespace Entitas {
    public partial class Pool {
        public ISystem CreateDelayedDestroySystem() {
            return this.CreateSystem<Assets.Features.Destruction.DelayedDestroySystem>();
        }
    }
}