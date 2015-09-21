namespace Entitas {
    public partial class Pool {
        public ISystem CreateArrowStickToCollidedSystem() {
            return this.CreateSystem<ArrowStickToCollidedSystem>();
        }
    }
}