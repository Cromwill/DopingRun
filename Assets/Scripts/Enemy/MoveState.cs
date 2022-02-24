using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveState : State
{
    [SerializeField] private SumoFighter _enemy;

    private float _speed;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _speed = Random.Range(5, 11);
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
