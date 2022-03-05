using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour, IPushable
{
    [SerializeField] private float _pushTime;

    private Rigidbody _rigidbody;
    private float _pushSpeed;
    private bool _isPushed;

    public event Action PushEnd;
    public event Action PushStart;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_isPushed)
            _rigidbody.MovePosition(transform.position - transform.forward * _pushSpeed * Time.deltaTime);
    }

    public void Push(Vector3 direction, float pushSpeed)
    {
        _pushSpeed = pushSpeed;
        StartCoroutine(PushAnimation());
    }

    private IEnumerator PushAnimation()
    {
        PushStart?.Invoke();
        _isPushed = true;

        yield return new WaitForSeconds(_pushTime);

        _isPushed = false;
        PushEnd?.Invoke();
    }
}
