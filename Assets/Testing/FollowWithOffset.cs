using UnityEngine;
using System.Collections;

public class FollowWithOffset : MonoBehaviour
{
    public Transform FollowObject;
    private Vector3 _offset;

    // Use this for initialization
	void Start ()
	{
	    _offset = transform.position - FollowObject.position;
	}

    // Update is called once per frame
	void Update ()
	{
	    transform.position = FollowObject.position + _offset;
	}
}
