namespace Entitas {
    public partial class Pool {
        public ISystem CreateAnimateUnitSystem() {
            return this.CreateSystem<AnimateUnitSystem>();
        }
    }
}