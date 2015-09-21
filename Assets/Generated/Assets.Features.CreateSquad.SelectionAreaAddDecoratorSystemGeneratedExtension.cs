namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectionAreaAddDecoratorSystem() {
            return this.CreateSystem<Assets.Features.CreateSquad.SelectionAreaAddDecoratorSystem>();
        }
    }
}