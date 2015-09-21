namespace Entitas {
    public partial class Pool {
        public ISystem CreateAttachToSystem() {
            return this.CreateSystem<Assets.Features.Attack.AttachToSystem>();
        }
    }
}