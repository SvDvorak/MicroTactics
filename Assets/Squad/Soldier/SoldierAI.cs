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
    public float Health = 100;
    public Vector3 SquadPosition;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Quaternion _squadOrientation;
    private Animator _animator;
    private float _delay;
    private Rigidbody _rigidBody;
    private Vector3 _aimTarget;

    public bool IsDead { get { return Health <= 0; } }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
        _squadOrientation = transform.rotation;
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var isMoving = Vector3.Distance(transform.position, _targetPosition) > 0.001f;
        _animator.SetBool("IsMoving", isMoving && !IsDead);

        if (IsDead)
        {
            return;
        }

        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, TurnSpeed * Time.deltaTime);
    }

    public void MoveUnit(SoldierMoveOrder moveOrder)
    {
        _targetPosition = moveOrder.MovePosition + moveOrder.SquadOrientation * SquadPosition;
        _squadOrientation = moveOrder.SquadOrientation;
        _delay = Random.Range(0, MaxThinkDelay);
    }

    public void AimAt(Vector3 target)
    {
        _aimTarget = target + _squadOrientation * SquadPosition - transform.position;
        _targetRotation = Quaternion.LookRotation(_aimTarget, Vector3.up);
    }

    public void FireArrow()
    {
        if (IsDead)
        {
            return;
        }

        var arrow = (Rigidbody)Instantiate(ArrowTemplate, ArrowSpawnPoint.position, transform.rotation);

        var requiredForce = CalculateForce(transform.rotation, _aimTarget.magnitude, arrow.mass);
        arrow.AddForce(requiredForce);
    }

    public void ArrowHit(Vector3 arrowForce)
    {
        var wasJustAlive = !IsDead;
        Health -= arrowForce.magnitude;
        if (IsDead)
        {
            if (wasJustAlive)
            {
                SendMessageUpwards("UnitDied", gameObject);
            }

            _rigidBody.isKinematic = false;
            _rigidBody.AddForce(arrowForce*20);
        }
    }

    public void ArrowStuck(Vector3 arrowForce)
    {
        ArrowHit(arrowForce * 1.5f);
    }

    private Vector3 CalculateForce(Quaternion targetRotation, float targetDistance, float mass)
    {
        // Algorithm taken from http://en.wikipedia.org/wiki/Trajectory_of_a_projectile
        var verticalAimRotation = Quaternion.AngleAxis(FireAngle, Vector3.left);
        var fireDirection = targetRotation * verticalAimRotation * Vector3.forward;
        var requiredVelocity = Mathf.Sqrt((targetDistance * Physics.gravity.magnitude) / Mathf.Sin(Mathf.Deg2Rad * FireAngle * 2));
        var requiredForce = fireDirection * requiredVelocity * (1 / Time.fixedDeltaTime) * mass;
        return requiredForce;
    }
}