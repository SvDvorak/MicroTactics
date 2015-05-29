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
        transform.forward = _rigidBody.velocity.normalized;
    }
}
