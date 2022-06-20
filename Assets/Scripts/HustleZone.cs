using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class HustleZone : MonoBehaviour
{
    [SerializeField] private float _pushSpeed = 25;
    [SerializeField] private float _syringeCoeficient;
    [SerializeField] private float _cooldown;
    [SerializeField] private ParticleSystem _hitParticles;

    private float _expirationTime;
    private BoxCollider _boxCollider;
    private const float AnimationDelay = 0.23f;
    private bool _isPushSpeedSetted;

    public event UnityAction CollidedWithPushable;
    public event UnityAction CollidedWithTouchable;
    public event UnityAction CollidedWithBreakable;

    private void Start()
    {
        if (_syringeCoeficient <= 0)
            _syringeCoeficient = 1;

        _boxCollider = GetComponent<BoxCollider>();
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

    public void IncreaseCollider()
    {
        _boxCollider.size = new Vector3(1.2f, _boxCollider.size.y, _boxCollider.size.z);
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

    public void AddPushSpeed(float syringeCollected)
    {
        if (_isPushSpeedSetted)
            return;

        _pushSpeed += syringeCollected * _syringeCoeficient;
    }

    private bool IsOnCooldown()
    {
        return _expirationTime <= Time.time;
    }
}
