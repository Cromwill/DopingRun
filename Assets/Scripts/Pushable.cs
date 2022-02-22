using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour
{
    [SerializeField] private float _pushTime;
    [SerializeField] private float _pushSpeed;

    private Rigidbody _rigidbody;

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
        float timePassed = 0;

        while(timePassed< _pushTime)
        {
            timePassed += Time.deltaTime;

            _rigidbody.MovePosition(transform.position + direction * _pushSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
