using UnityEngine;
using System.Collections;
using System.Linq;

public class ArrowCollision : MonoBehaviour
{
    public float ImpulseRequiredToStick;
    public float StickDamageMultiplier;

    private bool _isStuck;
    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_isStuck)
        {
            return;
        }

        var otherCollider = collision.contacts.First().otherCollider;
        var isGround = otherCollider.gameObject.layer == LayerMask.NameToLayer("Ground");
        var isSoldier = otherCollider.gameObject.layer == LayerMask.NameToLayer("Soldier");

        var arrowForce = collision.relativeVelocity.magnitude/Time.fixedDeltaTime*_rigidBody.mass;
        var hitAngle = Vector3.Dot(collision.contacts.First().normal.normalized, collision.relativeVelocity.normalized);
        var angledArrowForce = arrowForce*hitAngle;
        var damage = angledArrowForce;

        if (isGround || isSoldier && ImpulseRequiredToStick < angledArrowForce)
        {
            StickToSoldier(otherCollider);
            damage *= StickDamageMultiplier;
        }

        if (isSoldier)
        {
            otherCollider.SendMessageUpwards("ArrowHit", damage);
        }
    }

    private void StickToSoldier(Collider otherCollider)
    {
        _rigidBody.isKinematic = true;
        _rigidBody.detectCollisions = false;
        _isStuck = true;
        transform.SetParent(otherCollider.transform, true);
    }
}
