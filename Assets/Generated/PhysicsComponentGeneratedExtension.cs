using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PhysicsComponent physics { get { return (PhysicsComponent)GetComponent(ComponentIds.Physics); } }

        public bool hasPhysics { get { return HasComponent(ComponentIds.Physics); } }

        static readonly Stack<PhysicsComponent> _physicsComponentPool = new Stack<PhysicsComponent>();

        public static void ClearPhysicsComponentPool() {
            _physicsComponentPool.Clear();
        }

        public Entity AddPhysics(UnityEngine.Rigidbody newRigidBody) {
            var component = _physicsComponentPool.Count > 0 ? _physicsComponentPool.Pop() : new PhysicsComponent();
            component.RigidBody = newRigidBody;
            return AddComponent(ComponentIds.Physics, component);
        }

        public Entity ReplacePhysics(UnityEngine.Rigidbody newRigidBody) {
            var previousComponent = hasPhysics ? physics : null;
            var component = _physicsComponentPool.Count > 0 ? _physicsComponentPool.Pop() : new PhysicsComponent();
            component.RigidBody = newRigidBody;
            ReplaceComponent(ComponentIds.Physics, component);
            if (previousComponent != null) {
                _physicsComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePhysics() {
            var component = physics;
            RemoveComponent(ComponentIds.Physics);
            _physicsComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherPhysics;

        public static AllOfMatcher Physics {
            get {
                if (_matcherPhysics == null) {
                    _matcherPhysics = new Matcher(ComponentIds.Physics);
                }

                return _matcherPhysics;
            }
        }
    }
}
