using UnityEngine;

public class SquadMove : MonoBehaviour
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

        if (_squadState.InteractState == SquadState.State.Attack || !possibleHit.HasValue)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _dragStartPoint = possibleHit.Value.point;
            _squadState.InteractState = SquadState.State.Move;
            MoveArrow.IsVisible = true;
        }
        else if (Input.GetMouseButtonUp(0) && _squadState.InteractState == SquadState.State.Move)
        {
            var lookDirection = (possibleHit.Value.point - _dragStartPoint).normalized;
            var squadTowardsBackRotation = Quaternion.LookRotation(-lookDirection, Vector3.up);

            _squadState.PerformForEachUnit((x, y) => SetUnitPositionInSquad(x, y, squadTowardsBackRotation));
            _squadState.InteractState = SquadState.State.Idle;
            MoveArrow.IsVisible = false;
        }

        if (_squadState.InteractState == SquadState.State.Move)
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