namespace Entitas {
    public partial class Pool {
        public ISystem CreatePitchFromVelocitySystem() {
            return this.CreateSystem<PitchFromVelocitySystem>();
        }
    }
}