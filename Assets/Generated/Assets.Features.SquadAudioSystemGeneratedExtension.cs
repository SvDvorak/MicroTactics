namespace Entitas {
    public partial class Pool {
        public ISystem CreateSquadAudioSystem() {
            return this.CreateSystem<Assets.Features.SquadAudioSystem>();
        }
    }
}