using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputComponent input { get { return (InputComponent)GetComponent(ComponentIds.Input); } }

        public bool hasInput { get { return HasComponent(ComponentIds.Input); } }

        static readonly Stack<InputComponent> _inputComponentPool = new Stack<InputComponent>();

        public static void ClearInputComponentPool() {
            _inputComponentPool.Clear();
        }

        public Entity AddInput(InputState newState, System.Collections.Generic.List<EntityHit> newEntitiesHit) {
            var component = _inputComponentPool.Count > 0 ? _inputComponentPool.Pop() : new InputComponent();
            component.State = newState;
            component.EntitiesHit = newEntitiesHit;
            return AddComponent(ComponentIds.Input, component);
        }

        public Entity ReplaceInput(InputState newState, System.Collections.Generic.List<EntityHit> newEntitiesHit) {
            var previousComponent = hasInput ? input : null;
            var component = _inputComponentPool.Count > 0 ? _inputComponentPool.Pop() : new InputComponent();
            component.State = newState;
            component.EntitiesHit = newEntitiesHit;
            ReplaceComponent(ComponentIds.Input, component);
            if (previousComponent != null) {
                _inputComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInput() {
            var component = input;
            RemoveComponent(ComponentIds.Input);
            _inputComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity inputEntity { get { return GetGroup(Matcher.Input).GetSingleEntity(); } }

        public InputComponent input { get { return inputEntity.input; } }

        public bool hasInput { get { return inputEntity != null; } }

        public Entity SetInput(InputState newState, System.Collections.Generic.List<EntityHit> newEntitiesHit) {
            if (hasInput) {
                throw new SingleEntityException(Matcher.Input);
            }
            var entity = CreateEntity();
            entity.AddInput(newState, newEntitiesHit);
            return entity;
        }

        public Entity ReplaceInput(InputState newState, System.Collections.Generic.List<EntityHit> newEntitiesHit) {
            var entity = inputEntity;
            if (entity == null) {
                entity = SetInput(newState, newEntitiesHit);
            } else {
                entity.ReplaceInput(newState, newEntitiesHit);
            }

            return entity;
        }

        public void RemoveInput() {
            DestroyEntity(inputEntity);
        }
    }

    public partial class Matcher {
        static AllOfMatcher _matcherInput;

        public static AllOfMatcher Input {
            get {
                if (_matcherInput == null) {
                    _matcherInput = new Matcher(ComponentIds.Input);
                }

                return _matcherInput;
            }
        }
    }
}
