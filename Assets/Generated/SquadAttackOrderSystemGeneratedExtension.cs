namespace Entitas {
    public partial class Pool {
        public ISystem CreateSquadAttackOrderSystem() {
            return this.CreateSystem<SquadAttackOrderSystem>();
        }
    }
}