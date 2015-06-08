using UnityEngine;
using System.Collections;

public class SquadDeselection : SquadInteractionBase
{
    private SquadState _squadState;
    private int _squadLayer;
    private int _groundLayer;

    void Start()
    {
        _squadLayer = LayerMask.NameToLayer("Squad");
        _groundLayer = LayerMask.NameToLayer("Ground");
        _squadState = GetComponent<SquadState>();
    }

    public override void MouseUp(RaycastHit value)
    {
        if(value.transform.gameObject.layer != _squadLayer)
        {
            _squadState.InteractState = Interaction.Unselected;
        }
    }

    public override int GetLayersToUse()
    {
        return 1 << _groundLayer | 1 << _squadLayer;
    }
}
