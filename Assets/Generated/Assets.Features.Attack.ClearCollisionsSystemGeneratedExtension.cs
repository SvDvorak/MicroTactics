namespace Entitas {
    public partial class Pool {
        public ISystem CreateClearCollisionsSystem() {
            return this.CreateSystem<Assets.Features.Attack.ClearCollisionsSystem>();
        }
    }
}