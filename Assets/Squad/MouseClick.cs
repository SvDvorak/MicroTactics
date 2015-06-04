using UnityEngine;
using System.Collections;
using System.Linq;

public class MouseClick : MonoBehaviour
{
    private SquadInteractionBase[] _interactions;
    private SquadInteractionBase _dominantInteraction;

    void Start ()
    {
        _interactions = GetComponentsInParent<SquadInteractionBase>();
    }

    public void Update()
    {
        if (_dominantInteraction != null && _dominantInteraction.IsDominant())
        {
            UpdateInteraction(_dominantInteraction);
        }
        else
        {
            UpdateAllInteractions();
        }
    }

    private void UpdateAllInteractions()
    {
        foreach (var interaction in _interactions)
        {
            if (!interaction.IsEnabled())
            {
                continue;
            }

            UpdateInteraction(interaction);

            if (interaction.IsDominant())
            {
                _dominantInteraction = interaction;
                break;
            }
        }
    }

    private void UpdateInteraction(SquadInteractionBase interaction)
    {
        var possibleHit = RaycastUsingCamera(interaction.GetLayersToUse());

        if (!possibleHit.HasValue)
        {
            return;
        }

        interaction.OnMouseUpdate(possibleHit.Value);

        if (Input.GetMouseButtonDown(0))
        {
            interaction.OnMouseDown(possibleHit.Value);
        }

        if (Input.GetMouseButtonUp(0))
        {
            interaction.OnMouseUp(possibleHit.Value);
        }
    }

    private Maybe<RaycastHit> RaycastUsingCamera(int layersToUse)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToUse))
        {
            return hit.ToMaybe();
        }

        return Maybe<RaycastHit>.Nothing;
    }
}
