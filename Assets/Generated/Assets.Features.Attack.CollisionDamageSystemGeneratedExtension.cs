namespace Entitas {
    public partial class Pool {
        public ISystem CreateCollisionDamageSystem() {
            return this.CreateSystem<Assets.Features.Attack.CollisionDamageSystem>();
        }
    }
}