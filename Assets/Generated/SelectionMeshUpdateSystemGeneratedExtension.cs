namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectionMeshUpdateSystem() {
            return this.CreateSystem<MeshUpdateSystem>();
        }
    }
}