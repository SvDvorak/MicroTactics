using UnityEngine;

public class SquadMove : SquadInteractionBase
{
    public Arrow MoveArrow;

    private Vector3 _dragStartPoint;
    private SquadState _squadState;
    private int _squadLayer;
    private int _groundLayer;
    private Vector3 _lastMousePosition;

    void Start()
    {
        _squadLayer = LayerMask.NameToLayer("Squad");
        _groundLayer = LayerMask.NameToLayer("Ground");
        _squadState = GetComponent<SquadState>();
    }

    private bool HasMovedMouseSinceClick { get { return (_lastMousePosition - _dragStartPoint).sqrMagnitude > 0.0001f; } }

    public override void MouseUpdate(RaycastHit value)
    {
        _lastMousePosition = value.point;
        var isMoving = _squadState.InteractState == Interaction.Move;

        if (isMoving && HasMovedMouseSinceClick)
        {
            MoveArrow.IsVisible = true;
            MoveArrow.SetPositions(_dragStartPoint, value.point);
        }
    }

    public override void MouseDown(RaycastHit value)
    {
        var isMoving = _squadState.InteractState == Interaction.Move;
        var isIdle = _squadState.InteractState == Interaction.Idle;
        var isSquadLayer = value.transform.gameObject.layer == _squadLayer;

        if (!isSquadLayer && (isMoving || isIdle))
        {
            _dragStartPoint = value.point;
            _squadState.InteractState = Interaction.Move;
        }
    }

    public override void MouseUp(RaycastHit value)
    {
        var isMoving = _squadState.InteractState == Interaction.Move;

        if (isMoving)
        {
            var lookDirection = (value.point - _dragStartPoint).normalized;
            _squadState.CenterRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            _squadState.Units.ForEach(x =>
                {
                    x.SendMessage("MoveUnit", new SoldierMoveOrder(_dragStartPoint, _squadState.CenterRotation));
                });
            _squadState.InteractState = Interaction.Idle;
            MoveArrow.IsVisible = false;
        }
    }

    public override bool IsDominant()
    {
        var isMoving = _squadState.InteractState == Interaction.Move;
        return isMoving && HasMovedMouseSinceClick;
    }

    public override int GetLayersToUse()
    {
        return 1 << _groundLayer | 1 << _squadLayer;
    }
}