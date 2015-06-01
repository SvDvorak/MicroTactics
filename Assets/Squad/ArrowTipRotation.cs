using UnityEngine;
using System.Collections;

public class ArrowTipRotation : MonoBehaviour
{
    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        if(Mathf.Abs(_rigidBody.velocity.sqrMagnitude) >= 0.001f)
        {
            transform.forward = _rigidBody.velocity.normalized;
        }
    }
}
