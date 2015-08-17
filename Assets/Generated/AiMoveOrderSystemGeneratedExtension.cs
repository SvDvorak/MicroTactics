namespace Entitas {
    public partial class Pool {
        public ISystem CreateAiMoveOrderSystem() {
            return this.CreateSystem<AiMoveOrderSystem>();
        }
    }
}