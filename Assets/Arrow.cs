using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public Transform ArrowBase;
    public Transform ArrowHead;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

    public void SetPositions(Vector3 start, Vector3 end)
    {
        var startToEnd = end - start;

        ArrowBase.transform.position = start;
        ArrowHead.transform.position = end;

        var scale = ArrowBase.transform.localScale;
        scale.z = startToEnd.magnitude - ArrowHead.localScale.z;
        ArrowBase.transform.localScale = scale;

        if (startToEnd.sqrMagnitude >= 0.001f)
        {
            var arrowRotation = Quaternion.LookRotation(startToEnd, Vector3.up);
            ArrowBase.rotation = arrowRotation;
            ArrowHead.rotation = arrowRotation;
        }
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}