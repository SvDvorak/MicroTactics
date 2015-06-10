using UnityEngine;
using System.Collections;
using System.Linq;

public class ArrowCollision : MonoBehaviour
{
    private bool _isStuck;
    private float _aliveTime;
    private const float TimeUntilActive = 1f;

    public void Start()
    {
        _aliveTime = 0;
    }

    public void Update()
    {
        _aliveTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        var otherCollider = collision.contacts.First().otherCollider;
        if(_aliveTime > TimeUntilActive && !_isStuck && !otherCollider.gameObject.name.Contains("Arrow"))
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            rigidBody.detectCollisions = false;
            _isStuck = true;
        }
    }
}
