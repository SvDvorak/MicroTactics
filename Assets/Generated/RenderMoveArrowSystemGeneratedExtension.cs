namespace Entitas {
    public partial class Pool {
        public ISystem CreateRenderMoveArrowSystem() {
            return this.CreateSystem<RenderMoveArrowSystem>();
        }
    }
}