using UnityEngine;

public class SquadAttack : SquadInteractionBase
{
    public Arrow AttackArrow;

    private SquadState _squadState;
    private int _squadLayer;
    private int _groundLayer;
    private Vector3 _lastMousePosition;

    void Start ()
    {
        _squadState = GetComponent<SquadState>();
        _squadLayer = LayerMask.NameToLayer("Squad");
        _groundLayer = LayerMask.NameToLayer("Ground");
    }

    public override void MouseUpdate(RaycastHit value)
    {
        _lastMousePosition = value.point;
        if (_squadState.InteractState == Interaction.Attack)
        {
            AttackArrow.IsVisible = true;
            AttackArrow.SetPositions(_squadState.CenterPosition, _lastMousePosition);
        }
    }

    public override void MouseDown(RaycastHit value)
    {
        var isThisSquad = value.transform.parent == transform;

        if (isThisSquad && _squadState.InteractState != Interaction.Unselected)
        {
            _squadState.InteractState = Interaction.Attack;
        }
    }

    public override void MouseUp(RaycastHit value)
    {
        var isGroundLayer = value.transform.gameObject.layer == _groundLayer;

        if (isGroundLayer && _squadState.InteractState == Interaction.Attack)
        {
            AttackArrow.IsVisible = false;
            _squadState.InteractState = Interaction.Idle;
            _squadState.PerformForEachUnit(FireArrow);
        }
    }

    public override bool IsDominant()
    {
        return _squadState.InteractState == Interaction.Attack;
    }

    public override int GetLayersToUse()
    {
        if (IsDominant())
        {
            return 1 << _groundLayer;
        }

        return 1 << _squadLayer;
    }

    private void FireArrow(int x, int y)
    {
        const int inversePositions = -1;
        var unitPositionOffset = new Vector3((x-_squadState.Columns/2)*_squadState.Spacing, 0, (y-_squadState.Rows/2)*_squadState.Spacing)*inversePositions;
        _squadState.Units[x, y].SendMessage("FireArrow", _lastMousePosition + _squadState.CenterRotation*unitPositionOffset);
    }
}