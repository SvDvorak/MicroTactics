namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectionAreaDecoratorSystem() {
            return this.CreateSystem<Assets.Features.CreateSquad.SelectionAreaDecoratorSystem>();
        }
    }
}