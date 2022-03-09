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
    [SerializeField] private ParticleSystem _hitParticles;

    private float _expirationTime;

    public event UnityAction CollidedWithPushable;
    public event UnityAction CollidedWithTouchable;
    public event UnityAction CollidedWithBreakable;

    private void Start()
    {
        if (_stepCoeficient <= 0)
            _stepCoeficient = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPushable pushable))
        {
            if(_hitParticles != null)
                _hitParticles.Play();

            Vector3 direction = (other.transform.position - transform.position).normalized;
            direction.y = 0f;

            if (pushable is Touchable)
            {
                CollidedWithPushable?.Invoke();
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
            CollidedWithBreakable?.Invoke();
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
