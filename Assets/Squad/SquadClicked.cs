using UnityEngine;
using System.Collections;
using System.Linq;

public class SquadClicked : MonoBehaviour
{
    private SquadInteractionBase[] _interactions;

    void Start ()
    {
        _interactions = GetComponentsInParent<SquadInteractionBase>();
    }

    public void OnMouseDown()
    {
        foreach (var interaction in _interactions)
        {
            interaction.OnMouseDown();
        }
    }

    public void OnMouseUp()
    {
        foreach (var interaction in _interactions)
        {
            interaction.OnMouseUp();
        }
    }
}
