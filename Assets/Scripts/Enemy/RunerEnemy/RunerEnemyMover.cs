using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RunerEnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GrabZone _grabZone;
    
    private Rigidbody _rigidbody;
    private RunerEnemyTrigger _enemyTrigger;

    public event Action Moving;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _enemyTrigger = GetComponentInChildren<RunerEnemyTrigger>();
        _enemyTrigger.PlayerInTriggerZone += Move;
        _grabZone.PlayerInGrabZone += Disable;
    }

    private void OnDisable()
    {
        _enemyTrigger.PlayerInTriggerZone -= Move;
        _grabZone.PlayerInGrabZone -= Disable;
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }

    private void Move(Vector3 direction)
    {
        _rigidbody.MovePosition(transform.position + direction * _speed * Time.deltaTime);
        Moving?.Invoke();
        Rotate(direction);
    }

    public void MoveForward()
    {
        StartCoroutine(MoveForwardAimation());
    }

    private IEnumerator MoveForwardAimation()
    {
        float timePassed = 0;

        while(timePassed < 0.1f)
        {
            _rigidbody.MovePosition(transform.position + (transform.right+transform.forward) * _speed*2f * Time.deltaTime);
            timePassed += Time.deltaTime;

            yield return null;
        }
    }

    private void Disable()
    {
        this.enabled = false;
    }
}
