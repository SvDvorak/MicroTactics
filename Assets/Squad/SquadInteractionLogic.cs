using UnityEngine;
using System.Collections;

public class SquadInteractionLogic : MonoBehaviour
{
    public bool IsSelected;

	void Start ()
	{
	}

	void Update ()
	{
	}

    public void OnMouseDown()
    {
    }

    public void OnMouseUp()
    {
        IsSelected = true;
    }
}