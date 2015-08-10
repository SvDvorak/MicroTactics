namespace Entitas {
    public partial class Pool {
        public ISystem CreateArrowTipRotationSystem() {
            return this.CreateSystem<PitchFromVelocitySystem>();
        }
    }
}