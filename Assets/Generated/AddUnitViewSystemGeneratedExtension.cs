namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddUnitViewSystem() {
            return this.CreateSystem<AddUnitViewSystem>();
        }
    }
}