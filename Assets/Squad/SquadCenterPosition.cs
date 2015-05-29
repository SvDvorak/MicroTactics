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

        _squadState.CenterPosition = summedPositions/count;

        Debug.DrawLine(_squadState.CenterPosition, _squadState.CenterPosition + _squadState.CenterRotation*Vector3.forward, Color.red);
    }
}
