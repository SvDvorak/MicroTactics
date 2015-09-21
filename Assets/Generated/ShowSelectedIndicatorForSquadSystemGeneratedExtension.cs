namespace Entitas {
    public partial class Pool {
        public ISystem CreateShowSelectedIndicatorForSquadSystem() {
            return this.CreateSystem<ShowSelectedIndicatorForSquadSystem>();
        }
    }
}