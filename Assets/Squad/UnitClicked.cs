using UnityEngine;
using System.Collections;

public class UnitClicked : MonoBehaviour
{
    private SquadAttack _squadAttack;
    private SquadState _squadState;
    private bool _wasJustSelected;

    void Start()
    {
        _squadAttack = GetComponentInParent<SquadAttack>();
        _squadState = GetComponentInParent<SquadState>();
    }

    public void OnMouseDown()
    {
        if (_squadState.InteractState == SquadState.Interaction.Unselected)
        {
            _wasJustSelected = true;
        }
        else
        {
            _squadAttack.OnMouseDown();
        }
    }

    public void OnMouseUp()
    {
        if (_wasJustSelected)
        {
            _squadState.InteractState = SquadState.Interaction.Idle;
        }
        else
        {
            _squadAttack.OnMouseUp();
        }
    }
}
