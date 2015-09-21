namespace Entitas {
    public partial class Pool {
        public ISystem CreateHideHiddenSystem() {
            return this.CreateSystem<HideHiddenSystem>();
        }
    }
}