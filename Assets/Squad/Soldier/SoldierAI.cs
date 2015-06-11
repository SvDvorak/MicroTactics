using UnityEngine;
using System.Collections;

public class SoldierAI : MonoBehaviour
{
    public Rigidbody ArrowTemplate;
    public Transform ArrowSpawnPoint;
    public float MoveSpeed = 4;
    public float TurnSpeed = 360;
    public float MaxThinkDelay = 1f;
    public float FireAngle = 45;

    private Vector3 _targetPosition;
    private Quaternion _targetOrientation;
    private Animator _animator;
    private float _delay;

    void Start ()
    {
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
        _targetOrientation = transform.rotation;
    }

    void Update ()
	{
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetOrientation, TurnSpeed * Time.deltaTime);

        var isMoving = Vector3.Distance(transform.position, _targetPosition) > 0.001f;
        _animator.SetBool("IsMoving", isMoving);
    }

    public void SetSquadPosition(SoldierMoveOrder moveOrder)
    {
        _targetPosition = moveOrder.NewSoldierPosition;
        _targetOrientation = moveOrder.SquadOrientation;
        _delay = Random.Range(0, MaxThinkDelay);
    }

    public void FireArrow(Vector3 target)
    {
        // Algorithm taken from http://en.wikipedia.org/wiki/Trajectory_of_a_projectile
        var toTarget = target - transform.position;
        var targetRotation = Quaternion.LookRotation(toTarget, Vector3.up);

        var arrow = (Rigidbody)Instantiate(ArrowTemplate, ArrowSpawnPoint.position, targetRotation);

        var requiredForce = CalculateForce(targetRotation, toTarget.magnitude, arrow.mass);
        arrow.AddForce(requiredForce);
    }

    private Vector3 CalculateForce(Quaternion targetRotation, float targetDistance, float mass)
    {
        var verticalAimRotation = Quaternion.AngleAxis(FireAngle, Vector3.left);
        var fireDirection = targetRotation*verticalAimRotation*Vector3.forward;
        var requiredVelocity = Mathf.Sqrt((targetDistance*Physics.gravity.magnitude)/Mathf.Sin(Mathf.Deg2Rad*FireAngle*2));
        var requiredForce = fireDirection*requiredVelocity*(1/Time.fixedDeltaTime)*mass;
        return requiredForce;
    }
}