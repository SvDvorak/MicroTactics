using UnityEngine;

public class SquadInteractionBase : MonoBehaviour
{
    public virtual void OnMouseUpdate(RaycastHit value) { }
    public virtual void OnMouseDown(RaycastHit value) { }
    public virtual void OnMouseUp(RaycastHit value) { }
    public virtual int GetLayersToUse() { return ~0; }
    public virtual bool IsDominant() { return false; }

    public bool IsEnabled()
    {
        return enabled;
    }
}