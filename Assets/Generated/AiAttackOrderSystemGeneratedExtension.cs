namespace Entitas {
    public partial class Pool {
        public ISystem CreateAiAttackOrderSystem() {
            return this.CreateSystem<AiAttackOrderSystem>();
        }
    }
}