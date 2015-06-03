using UnityEngine;
using System.Collections;

public class ShowAsSelected : MonoBehaviour
{
    public GameObject IndicatorObject;

    private SquadState _squadState;
    private bool _isSelected;

    void Start ()
    {
        _squadState = GetComponentInParent<SquadState>();
        _isSelected = false;
    }

    void Update ()
    {
        var isNowSelected = _squadState.InteractState != Interaction.Unselected;
        if (_isSelected != isNowSelected)
        {
            IndicatorObject.SetActive(isNowSelected);
            _isSelected = isNowSelected;
        }
	}
}
