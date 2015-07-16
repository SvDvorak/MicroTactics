using UnityEngine;
using System.Collections;

public class SimulateClick : MonoBehaviour
{
    public GameObject _clickObject;
    public float WaitTime;

    public IEnumerator Start()
    {
        var toClick = _clickObject.GetComponent<SquadInteractionLogic>();
        yield return new WaitForSeconds(WaitTime);
        toClick.OnMouseDown();
        toClick.OnMouseUp();
    }
}