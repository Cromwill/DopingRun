using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HustleZone : MonoBehaviour
{
    private float _cooldown;

    private float _expirationTime;

    public UnityAction CollideWithEnemy;

    private void Start()
    {
        _cooldown = Random.Range(0.2f, 0.8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPushable pushable))
        {
            Vector3 direcation = (other.transform.position - transform.position).normalized;

            CollideWithEnemy?.Invoke();

            if (IsOnCooldown())
            {
                _expirationTime = Time.time + _cooldown;
                pushable.Push(direcation);
            }
        }
    }

    private bool IsOnCooldown()
    {
        return _expirationTime <= Time.time;
    }
}
