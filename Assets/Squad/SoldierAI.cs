using UnityEngine;
using System.Collections;

public class SoldierAI : MonoBehaviour
{
    public Rigidbody ArrowTemplate;
    public float ArrowForce = 800;

    private const float MoveSpeed = 4;
    private Vector3 _targetPosition;
    private Animator _animator;
    private float _delay;

    void Start ()
    {
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
    }

    void Update ()
	{
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MoveSpeed*Time.deltaTime);

        var isMoving = Vector3.Distance(transform.position, _targetPosition) > 0.001f;
        _animator.SetBool("IsMoving", isMoving);
    }

    public void SetSquadPosition(Vector3 position)
    {
        _targetPosition = position;
        _delay = Random.Range(0, 1f);
    }

    public void FireArrow(Vector3 target)
    {
        var fireDirection = ((target - transform.position).normalized + Vector3.up).normalized;
        var arrowRotation = Quaternion.LookRotation(fireDirection, Vector3.up);
        var arrow = (Rigidbody)Instantiate(ArrowTemplate, transform.position, arrowRotation);
        arrow.AddForce(fireDirection*ArrowForce);
    }
}