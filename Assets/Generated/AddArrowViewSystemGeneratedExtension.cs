namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddArrowViewSystem() {
            return this.CreateSystem<AddArrowViewSystem>();
        }
    }
}