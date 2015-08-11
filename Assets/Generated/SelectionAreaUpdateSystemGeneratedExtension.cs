namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectionAreaUpdateSystem() {
            return this.CreateSystem<SelectionAreaUpdateSystem>();
        }
    }
}