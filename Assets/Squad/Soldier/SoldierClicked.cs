using UnityEngine;
using System.Collections;

public class SoldierClicked : MonoBehaviour
{
    private SquadClicked _squadClicked;

    void Start()
    {
        _squadClicked = GetComponentInParent<SquadClicked>();
    }

    public void OnMouseDown()
    {
        _squadClicked.OnMouseDown();
    }

    public void OnMouseUp()
    {
        _squadClicked.OnMouseUp();
    }
}
