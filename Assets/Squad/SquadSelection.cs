using UnityEngine;
using System.Collections;

public class SquadSelection : SquadInteractionBase
{
    private SquadState _squadState;
    private int _groupLayer;

    void Start ()
    {
        _groupLayer = 1 << LayerMask.NameToLayer("Squad");
        _squadState = GetComponent<SquadState>();
    }

    public override void MouseUp(RaycastHit value)
    {
        _squadState.InteractState = Interaction.Idle;
    }

    public override int GetLayersToUse()
    {
        return _groupLayer;
    }
}