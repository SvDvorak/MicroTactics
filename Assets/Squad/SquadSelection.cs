using UnityEngine;
using System.Collections;

public class SquadSelection : SquadInteractionBase
{
    private bool _wasJustSelected;
    private SquadState _squadState;

    void Start ()
    {
        _squadState = GetComponent<SquadState>();
    }

    void Update ()
	{
        if(_wasJustSelected)
        {
            _squadState.InteractState = Interaction.Idle;
            _wasJustSelected = false;
        }
	}

    public override void OnMouseUp()
    {
        if(_squadState.InteractState == Interaction.Unselected)
        {
            _wasJustSelected = true;
        }
    }
}
