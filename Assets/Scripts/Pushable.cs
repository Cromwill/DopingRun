using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour, IPushable
{
    [SerializeField] private float _pushTime;
    [SerializeField] private float _pushSpeed;

    private Rigidbody _rigidbody;

    public event Action PushEnd;
    public event Action PushStart;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 direction)
    {
        StartCoroutine(PushAnimation(direction));
    }

    private IEnumerator PushAnimation(Vector3 direction)
    {
        Debug.LogError("asd");
        float timePassed = 0;

        PushStart?.Invoke();

        while (timePassed< _pushTime)
        {
            timePassed += Time.deltaTime;

            _rigidbody.MovePosition(transform.position + direction * _pushSpeed * Time.deltaTime);

            yield return null;
        }

        PushEnd?.Invoke();
    }
}
