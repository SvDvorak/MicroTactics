using UnityEngine;
using System.Collections;

public class UnitClicked : MonoBehaviour
{
    private SquadAttack _squadAttack;

    void Start()
    {
        _squadAttack = GetComponentInParent<SquadAttack>();
    }

    public void OnMouseDown()
    {
        _squadAttack.OnMouseDown();
    }

    public void OnMouseUp()
    {
        _squadAttack.OnMouseUp();
    }
}
