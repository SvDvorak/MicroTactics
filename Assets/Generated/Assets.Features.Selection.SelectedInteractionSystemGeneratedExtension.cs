namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectedInteractionSystem() {
            return this.CreateSystem<Assets.Features.Selection.SelectedInteractionSystem>();
        }
    }
}