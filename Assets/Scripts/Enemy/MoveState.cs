using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Pushable))]
public class MoveState : State
{
    [SerializeField] private SumoFighter _enemy;

    private Pushable _pushable;
    private float _speed;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _speed = Random.Range(3, 4);
        _rigidBody = GetComponent<Rigidbody>();
        _pushable = GetComponent<Pushable>();
    }

    private void FixedUpdate()
    {
        if (_pushable.IsPushed)
            return;

        if(_enemy.Target != null)
        {
            Vector3 direction = (_enemy.Target.transform.position - transform.position).normalized;

            _rigidBody.MovePosition(transform.position + direction * _speed * Time.deltaTime);
            Rotate(direction);
        }
    }

    public void SetTarget(SumoFighter target)
    {
        Target = target;
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }
}
