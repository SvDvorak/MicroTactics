namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectionAreaRemoveDecoratorSystem() {
            return this.CreateSystem<Assets.Features.CreateSquad.SelectionAreaRemoveDecoratorSystem>();
        }
    }
}