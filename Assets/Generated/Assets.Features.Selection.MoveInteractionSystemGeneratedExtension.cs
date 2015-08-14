namespace Entitas {
    public partial class Pool {
        public ISystem CreateMoveInteractionSystem() {
            return this.CreateSystem<Assets.Features.Selection.MoveInteractionSystem>();
        }
    }
}