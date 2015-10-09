namespace Entitas {
    public partial class Pool {
        public ISystem CreateAttackInteractionSystem() {
            return this.CreateSystem<AttackInteractionSystem>();
        }
    }
}