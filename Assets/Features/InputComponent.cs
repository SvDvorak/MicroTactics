using System.Collections.Generic;
using Entitas;
using Mono.GameMath;

public class InputComponent : IComponent
{
    public InputState State;
    public List<EntityHit> EntitiesHit;
}

public class EntityHit
{
    public EntityHit(Entity entity, Vector3 position)
    {
        Entity = entity;
        Position = position;
    }

    public Entity Entity { get; private set; }
    public Vector3 Position { get; private set; }
}

public enum InputState
{
    Press,
    Release,
    Hover
}