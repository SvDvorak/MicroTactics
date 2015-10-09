namespace Entitas {
    public partial class Pool {
        public ISystem CreateRenderAttackArrowSystem() {
            return this.CreateSystem<RenderAttackArrowSystem>();
        }
    }
}