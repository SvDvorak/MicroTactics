using UnityEngine;
using System.Collections;

public class ShowAsSelected : MonoBehaviour
{
    public GameObject IndicatorObject;

    private SquadState _squadState;
    private SoldierAI _unitState;
    private bool _isShowingAsSelected;

    void Start ()
    {
        _squadState = GetComponentInParent<SquadState>();
        _unitState = GetComponentInParent<SoldierAI>();
        _isShowingAsSelected = false;
    }

    void Update ()
    {
        var shouldBeSelected = !_unitState.IsDead && _squadState.InteractState != Interaction.Unselected;

        if (_isShowingAsSelected != shouldBeSelected)
        {
            IndicatorObject.SetActive(shouldBeSelected);
            _isShowingAsSelected = shouldBeSelected;
        }
	}
}
