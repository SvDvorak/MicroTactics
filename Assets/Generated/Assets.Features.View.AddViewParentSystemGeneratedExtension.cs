namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddViewParentSystem() {
            return this.CreateSystem<Assets.Features.View.AddViewParentSystem>();
        }
    }
}