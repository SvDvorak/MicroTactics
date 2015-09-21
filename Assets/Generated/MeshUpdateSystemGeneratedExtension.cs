namespace Entitas {
    public partial class Pool {
        public ISystem CreateMeshUpdateSystem() {
            return this.CreateSystem<MeshUpdateSystem>();
        }
    }
}