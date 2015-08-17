using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public BoundingMeshComponent boundingMesh { get { return (BoundingMeshComponent)GetComponent(ComponentIds.BoundingMesh); } }

        public bool hasBoundingMesh { get { return HasComponent(ComponentIds.BoundingMesh); } }

        static readonly Stack<BoundingMeshComponent> _boundingMeshComponentPool = new Stack<BoundingMeshComponent>();

        public static void ClearBoundingMeshComponentPool() {
            _boundingMeshComponentPool.Clear();
        }

        public Entity AddBoundingMesh(System.Collections.Generic.List<Mono.GameMath.Vector2> newPoints) {
            var component = _boundingMeshComponentPool.Count > 0 ? _boundingMeshComponentPool.Pop() : new BoundingMeshComponent();
            component.Points = newPoints;
            return AddComponent(ComponentIds.BoundingMesh, component);
        }

        public Entity ReplaceBoundingMesh(System.Collections.Generic.List<Mono.GameMath.Vector2> newPoints) {
            var previousComponent = hasBoundingMesh ? boundingMesh : null;
            var component = _boundingMeshComponentPool.Count > 0 ? _boundingMeshComponentPool.Pop() : new BoundingMeshComponent();
            component.Points = newPoints;
            ReplaceComponent(ComponentIds.BoundingMesh, component);
            if (previousComponent != null) {
                _boundingMeshComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveBoundingMesh() {
            var component = boundingMesh;
            RemoveComponent(ComponentIds.BoundingMesh);
            _boundingMeshComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherBoundingMesh;

        public static AllOfMatcher BoundingMesh {
            get {
                if (_matcherBoundingMesh == null) {
                    _matcherBoundingMesh = new Matcher(ComponentIds.BoundingMesh);
                }

                return _matcherBoundingMesh;
            }
        }
    }
}
