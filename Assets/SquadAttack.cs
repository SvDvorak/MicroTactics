using UnityEngine;
using System.Collections;

public class SquadAttack : MonoBehaviour
{
    public Arrow AttackArrow;

    private SquadState _squadState;
    private int _groundLayer;

    void Start ()
    {
        _squadState = GetComponent<SquadState>();
        _groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update ()
	{
        if (_squadState.InteractState == SquadState.State.Attack)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                AttackArrow.SetPositions(_squadState.CenterPosition, hit.point);
            }
        }
    }

    public void OnMouseDown()
    {
        AttackArrow.IsVisible = true;
        _squadState.InteractState = SquadState.State.Attack;
    }

    public void OnMouseUp()
    {
        AttackArrow.IsVisible = false;
        _squadState.InteractState = SquadState.State.Idle;
    }
}