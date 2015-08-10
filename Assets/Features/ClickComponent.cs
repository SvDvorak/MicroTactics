using System.Collections.Generic;
using Entitas;

public class InputPressComponent : InputComponent, IComponent
{

}

public class InputReleaseComponent : InputComponent, IComponent
{

}

public class InputPositionChangedComponent : InputComponent, IComponent
{
}

public class InputComponent
{
    public List<Entity> Entities;
}