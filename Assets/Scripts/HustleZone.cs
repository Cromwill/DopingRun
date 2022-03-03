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
    
    private float _expirationTime;

    public UnityAction CollidedWithPushable;
    public UnityAction CollidedWithTouchable;

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

            if (pushable is Touchable)
            {
                CollidedWithTouchable?.Invoke();
                pushable.Push(direction, _pushSpeed);
            }

            if(pushable is Pushable)
                TryPush(pushable, direction);

            if (pushable is BreakablePiece)
                Break(pushable, direction);
        }
    }

    private void TryPush(IPushable pushable, Vector3 direction)
    {
        if (IsOnCooldown())
        {
            CollidedWithPushable?.Invoke();
            _expirationTime = Time.time + _cooldown;
            pushable.Push(direction, _pushSpeed);
        }
    }

    private void Break(IPushable pushable, Vector3 direction)
    {
        pushable.Push(direction, _pushSpeed);

        if (IsOnCooldown())
        {
            CollidedWithPushable?.Invoke();
            _expirationTime = Time.time + _cooldown;
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
