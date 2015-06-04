using UnityEngine;
using System.Collections;
using System.Linq;

public class ArrowStickOnHit : MonoBehaviour
{
    private bool _isStuck;

    void OnCollisionEnter(Collision collision)
    {
        var otherCollider = collision.contacts.First().otherCollider;
        if(!_isStuck && !otherCollider.gameObject.name.Contains("Arrow"))
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            rigidBody.detectCollisions = false;
            _isStuck = true;
        }
    }
}
