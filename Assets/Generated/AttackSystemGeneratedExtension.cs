namespace Entitas {
    public partial class Pool {
        public ISystem CreateAttackSystem() {
            return this.CreateSystem<AttackSystem>();
        }
    }
}