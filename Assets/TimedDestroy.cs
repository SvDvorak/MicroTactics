using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour
{
    public float Delay = 3;

	void Start ()
    {
        Destroy(gameObject, Delay);
	}
}
