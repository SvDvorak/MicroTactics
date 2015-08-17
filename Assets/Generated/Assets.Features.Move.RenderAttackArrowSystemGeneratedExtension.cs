namespace Entitas {
    public partial class Pool {
        public ISystem CreateRenderAttackArrowSystem() {
            return this.CreateSystem<Assets.Features.Move.RenderAttackArrowSystem>();
        }
    }
}