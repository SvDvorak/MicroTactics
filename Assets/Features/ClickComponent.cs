using System.Collections.Generic;
using Entitas;

public class InputComponent : IComponent
{
    public InputState State;
    public List<Entity> Entities;
}

public enum InputState
{
    Press,
    Release,
    Hover
}