using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FirstGearGames.SmoothCameraShaker;

public class HustleZone : MonoBehaviour
{
    [SerializeField] private float _pushSpeed = 25;
    [SerializeField] private float _stepCoeficient;
    [SerializeField] private float _cooldown;
    [SerializeField] private ShakeData _shakeData;
    
    private float _expirationTime;

    public UnityAction CollidedWithPushable;
    public UnityAction<ShakeData> PlayerCollidedWithPushable;

    private void Start()
    {
        if (_stepCoeficient <= 0)
            _stepCoeficient = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPushable pushable))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0f;

            CollidedWithPushable?.Invoke();

            if (IsOnCooldown())
            {
                if(_shakeData != null)
                    PlayerCollidedWithPushable?.Invoke(_shakeData);

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
