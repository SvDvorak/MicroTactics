using UnityEngine;
using System.Collections;

public class SquadCenterPosition : MonoBehaviour
{
    private SquadState _squadState;

    void Start ()
    {
        _squadState = GetComponentInParent<SquadState>();
    }

    void Update ()
    {
        var count = 0;
        var summedPositions = Vector3.zero;
	    _squadState.Units.Iterate(unit =>
	        {
	            count++;
	            summedPositions += unit.transform.position;
	        });

        var newCenterPosition = summedPositions/count;
        _squadState.IsMoving = (_squadState.CenterPosition - newCenterPosition).magnitude > 0.0001f;
        _squadState.CenterPosition = newCenterPosition;

        Debug.DrawLine(_squadState.CenterPosition, _squadState.CenterPosition + _squadState.CenterRotation*Vector3.forward, Color.red);
    }
}
