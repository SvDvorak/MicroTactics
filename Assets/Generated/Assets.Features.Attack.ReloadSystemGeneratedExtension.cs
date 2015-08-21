namespace Entitas {
    public partial class Pool {
        public ISystem CreateReloadSystem() {
            return this.CreateSystem<Assets.Features.Attack.ReloadSystem>();
        }
    }
}