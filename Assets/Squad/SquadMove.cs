using UnityEngine;

public class SquadMove : SquadInteractionBase
{
    public Arrow MoveArrow;

    private Vector3 _dragStartPoint;
    private int _groundLayer;
    private SquadState _squadState;

    void Start()
    {
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
        _squadState = GetComponent<SquadState>();
    }

    private void Update()
    {
        var possibleHit = RaycastUsingCamera();

        var isMoving = _squadState.InteractState == Interaction.Move;
        var isIdle = _squadState.InteractState == Interaction.Idle;
        if (!isIdle && !isMoving || !possibleHit.HasValue)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _dragStartPoint = possibleHit.Value.point;
            _squadState.InteractState = Interaction.Move;
            MoveArrow.IsVisible = true;
        }
        else if (Input.GetMouseButtonUp(0) && isMoving)
        {
            var lookDirection = (possibleHit.Value.point - _dragStartPoint).normalized;
            _squadState.CenterRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            var squadTowardsBackRotation = _squadState.CenterRotation*Quaternion.AngleAxis(180, Vector3.up);

            _squadState.PerformForEachUnit((x, y) => SetUnitPositionInSquad(x, y, squadTowardsBackRotation));
            _squadState.InteractState = Interaction.Idle;
            MoveArrow.IsVisible = false;
        }

        if (isMoving)
        {
            MoveArrow.SetPositions(_dragStartPoint, possibleHit.Value.point);
        }
    }

    private Maybe<RaycastHit> RaycastUsingCamera()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
        {
            return hit.ToMaybe();
        }

        return Maybe<RaycastHit>.Nothing;
    }

    private void SetUnitPositionInSquad(int x, int y, Quaternion squadTowardsBackRotation)
    {
        var centerHitpoint = new Vector3((_squadState.Columns - 1)/2f, 0, 0)*_squadState.Spacing;
        var unitInSquadPosition = new Vector3(x, 0, y)*_squadState.Spacing;
        _squadState.Units[x, y].SendMessage("SetSquadPosition",
            _dragStartPoint + squadTowardsBackRotation*(unitInSquadPosition - centerHitpoint));
    }
}