namespace Entitas {
    public partial class Pool {
        public ISystem CreateUnitsCacheSystem() {
            return this.CreateSystem<UnitsCacheSystem>();
        }
    }
}