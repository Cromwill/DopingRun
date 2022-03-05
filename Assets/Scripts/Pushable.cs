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
    public bool IsPushed { get; private set; }

    public event Action PushEnd;
    public event Action PushStart;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(IsPushed)
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
        IsPushed = true;

        yield return new WaitForSeconds(_pushTime);

        IsPushed = false;
        PushEnd?.Invoke();
    }
}
