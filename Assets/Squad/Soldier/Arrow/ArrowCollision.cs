using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ArrowCollision : MonoBehaviour
{
    public float ImpulseRequiredToStick;

    private bool _isStuck;
    private Rigidbody _rigidBody;
    private Dictionary<int, float> _objectTypeHardness;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _objectTypeHardness = new Dictionary<int, float>
        {
            { LayerMask.NameToLayer("Ground"), 1 },
            { LayerMask.NameToLayer("Soldier"), ImpulseRequiredToStick }
        };
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_isStuck)
        {
            return;
        }

        var otherCollider = collision.contacts.First().otherCollider;
        var angledArrowForce = CalculateHitForce(collision);

        if (_objectTypeHardness.ContainsKey(otherCollider.gameObject.layer))
        {
            var requiredForceToStick = _objectTypeHardness[otherCollider.gameObject.layer];
            if (requiredForceToStick < angledArrowForce)
            {
                StickToObject(otherCollider);
                otherCollider.SendMessageUpwards("ArrowStuck", collision.relativeVelocity);
            }
            else
            {
                otherCollider.SendMessageUpwards("ArrowHit", collision.relativeVelocity);
            }
        }
    }

    private float CalculateHitForce(Collision collision)
    {
        var arrowForce = collision.relativeVelocity.magnitude/Time.fixedDeltaTime*_rigidBody.mass;
        var hitAngle = Vector3.Dot(collision.contacts.First().normal.normalized, collision.relativeVelocity.normalized);
        var angledArrowForce = arrowForce*hitAngle;
        return angledArrowForce;
    }

    private void StickToObject(Collider otherCollider)
    {
        _rigidBody.isKinematic = true;
        _rigidBody.detectCollisions = false;
        _isStuck = true;
        transform.SetParent(otherCollider.transform, true);
    }
}
