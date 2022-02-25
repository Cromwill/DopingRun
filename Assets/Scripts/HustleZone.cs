using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HustleZone : MonoBehaviour
{
    [SerializeField] private float _pushSpeed = 25;
    [SerializeField] private float _stepCoeficient;
    
    private float _cooldown;
    private float _expirationTime;

    public UnityAction CollideWithEnemy;

    private void Start()
    {
        if (_stepCoeficient <= 0)
            _stepCoeficient = 1;

        _cooldown = Random.Range(0.2f, 0.8f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPushable pushable))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0f;

            CollideWithEnemy?.Invoke();

            if (IsOnCooldown())
            {
                _expirationTime = Time.time + _cooldown;
                pushable.Push(direction, _pushSpeed);
            }
        }
    }

    public void AddPushSpeed(float pushSpeed)
    {
        Debug.Log("asdhasidfahsfuiashi");
        _pushSpeed += pushSpeed * _stepCoeficient;
    }

    private bool IsOnCooldown()
    {
        return _expirationTime <= Time.time;
    }
}
