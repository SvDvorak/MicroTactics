namespace Entitas {
    public partial class Pool {
        public ISystem CreateLinkViewsStartSystem() {
            return this.CreateSystem<LinkViewsStartSystem>();
        }
    }
}