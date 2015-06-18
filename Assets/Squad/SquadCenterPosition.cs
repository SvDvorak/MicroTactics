using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SquadCenterPosition : MonoBehaviour
{
    private SquadState _squadState;
    private Vector3 _previousCenterPosition;

    void Start ()
    {
        _squadState = GetComponentInParent<SquadState>();
    }

    void Update ()
    {
        _squadState.IsMoving = (_squadState.CenterPosition - _previousCenterPosition).magnitude > 0.0001f;
        transform.position = _squadState.CenterPosition;

        _previousCenterPosition = _squadState.CenterPosition;

        Debug.DrawLine(_squadState.CenterPosition, _squadState.CenterPosition + _squadState.CenterRotation*Vector3.forward, Color.red);
    }

    public static Vector3 GetCenterPoint(IEnumerable<Vector3> points)
    {
        var count = 0;
        var summedPositions = Vector3.zero;

        foreach (var point in points)
        {
            count++;
            summedPositions += point;
        }

        return summedPositions/count;
    }
}
