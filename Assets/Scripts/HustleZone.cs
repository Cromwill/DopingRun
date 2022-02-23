using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HustleZone : MonoBehaviour
{
    private float _cooldown;

    private float _expirationTime;

    private void Start()
    {
        _cooldown = Random.Range(0.2f, 0.8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pushable pushable))
        {
            Vector3 direcation = (other.transform.position - transform.position).normalized;

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
