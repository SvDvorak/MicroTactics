namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectInteractionSystem() {
            return this.CreateSystem<Assets.Features.Selection.SelectInteractionSystem>();
        }
    }
}