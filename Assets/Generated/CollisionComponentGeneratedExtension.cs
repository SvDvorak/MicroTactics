using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CollisionComponent collision { get { return (CollisionComponent)GetComponent(ComponentIds.Collision); } }

        public bool hasCollision { get { return HasComponent(ComponentIds.Collision); } }

        static readonly Stack<CollisionComponent> _collisionComponentPool = new Stack<CollisionComponent>();

        public static void ClearCollisionComponentPool() {
            _collisionComponentPool.Clear();
        }

        public Entity AddCollision(Entitas.Entity newEntity, UnityEngine.GameObject newView, Mono.GameMath.Vector3 newRelativeVelocity) {
            var component = _collisionComponentPool.Count > 0 ? _collisionComponentPool.Pop() : new CollisionComponent();
            component.Entity = newEntity;
            component.View = newView;
            component.RelativeVelocity = newRelativeVelocity;
            return AddComponent(ComponentIds.Collision, component);
        }

        public Entity ReplaceCollision(Entitas.Entity newEntity, UnityEngine.GameObject newView, Mono.GameMath.Vector3 newRelativeVelocity) {
            var previousComponent = hasCollision ? collision : null;
            var component = _collisionComponentPool.Count > 0 ? _collisionComponentPool.Pop() : new CollisionComponent();
            component.Entity = newEntity;
            component.View = newView;
            component.RelativeVelocity = newRelativeVelocity;
            ReplaceComponent(ComponentIds.Collision, component);
            if (previousComponent != null) {
                _collisionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCollision() {
            var component = collision;
            RemoveComponent(ComponentIds.Collision);
            _collisionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherCollision;

        public static AllOfMatcher Collision {
            get {
                if (_matcherCollision == null) {
                    _matcherCollision = new Matcher(ComponentIds.Collision);
                }

                return _matcherCollision;
            }
        }
    }
}
