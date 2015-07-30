using UnityEngine;
using System.Collections.Generic;
using Entitas;
using Vexe.Runtime.Types;

public class ComponentContainer : BetterBehaviour
{
    public List<IComponent> Components;
}