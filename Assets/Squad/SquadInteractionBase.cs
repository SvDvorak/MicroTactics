using UnityEngine;

public class SquadInteractionBase : MonoBehaviour
{
    public virtual void MouseUpdate(RaycastHit value) { }
    public virtual void MouseDown(RaycastHit value) { }
    public virtual void MouseUp(RaycastHit value) { }
    public virtual int GetLayersToUse() { return ~0; }
    public virtual bool IsDominant() { return false; }

    public bool IsEnabled()
    {
        return enabled;
    }
}