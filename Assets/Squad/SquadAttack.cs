﻿using UnityEngine;

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
            AttackArrow.SetArrowPoints(_squadState.CenterPosition, _lastMousePosition);
            _squadState.Units.ForEach(unit =>
                {
                    unit.SendMessage("AimAt", _lastMousePosition);
                });
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
            _squadState.Units.ForEach(unit =>
                {
                    unit.SendMessage("FireArrow");
                });
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
}