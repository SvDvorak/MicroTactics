using UnityEngine;
using System.Collections;

public class SquadAttack : MonoBehaviour
{
    public Arrow AttackArrow;

    private SquadState _squadState;
    private int _groundLayer;
    private Vector3 _lastMousePosition;

    void Start ()
    {
        _squadState = GetComponent<SquadState>();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update ()
	{
        if (_squadState.InteractState == SquadState.Interaction.Attack)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                _lastMousePosition = hit.point;
                AttackArrow.SetPositions(_squadState.CenterPosition, _lastMousePosition);
            }
        }
    }

    public void OnMouseDown()
    {
        AttackArrow.IsVisible = true;
        _squadState.InteractState = SquadState.Interaction.Attack;
    }

    public void OnMouseUp()
    {
        AttackArrow.IsVisible = false;
        _squadState.InteractState = SquadState.Interaction.Idle;
        _squadState.PerformForEachUnit(FireArrow);
    }

    private void FireArrow(int x, int y)
    {
        const int inversePositions = -1;
        var unitPositionOffset = new Vector3((x-_squadState.Columns/2)*_squadState.Spacing, 0, (y-_squadState.Rows/2)*_squadState.Spacing)*inversePositions;
        _squadState.Units[x, y].SendMessage("FireArrow", _lastMousePosition + _squadState.CenterRotation*unitPositionOffset);
    }
}