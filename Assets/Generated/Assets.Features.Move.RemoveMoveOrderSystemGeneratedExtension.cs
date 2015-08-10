namespace Entitas {
    public partial class Pool {
        public ISystem CreateRemoveMoveOrderSystem() {
            return this.CreateSystem<Assets.Features.Move.RemoveMoveOrderSystem>();
        }
    }
}