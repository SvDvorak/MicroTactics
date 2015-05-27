using UnityEngine;
using System.Collections;

public class SoldierAI : MonoBehaviour
{
    private const float MoveSpeed = 4;
    private Vector3 _targetPosition;
    private Animator _animator;
    private float _delay;

    void Start ()
    {
        _animator = GetComponent<Animator>();
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
}