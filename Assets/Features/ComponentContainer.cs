using UnityEngine;
using System.Collections.Generic;
using Entitas;
using Vexe.Runtime.Types;

public class ComponentContainer : BetterBehaviour
{
    [Display(Seq.Advanced | Seq.PerItemRemove | Seq.PerItemDuplicate)]
    public List<IComponent> Components;
}