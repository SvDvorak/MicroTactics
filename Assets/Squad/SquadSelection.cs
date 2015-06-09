using UnityEngine;
using System.Collections;

public class SquadSelection : SquadInteractionBase
{
    private SquadState _squadState;
    private int _groupLayer;
    private bool _justSelected;

    void Start ()
    {
        _groupLayer = 1 << LayerMask.NameToLayer("Squad");
        _squadState = GetComponent<SquadState>();
    }

    public override void MouseUp(RaycastHit value)
    {
        if (value.transform.parent == transform)
        {
            _squadState.InteractState = Interaction.Idle;
            _justSelected = true;
        }
    }

    public override int GetLayersToUse()
    {
        return _groupLayer;
    }

    public override bool IsDominant()
    {
        if (_justSelected)
        {
            _justSelected = false;
            return true;
        }

        return false;
    }
}