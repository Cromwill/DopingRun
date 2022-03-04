using UnityEngine;

namespace RunnerMovementSystem.Examples
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [Space(15)]
        [SerializeField] private Transform _target;
        [SerializeField] private float _height;
        [SerializeField] private float _distance;
        [SerializeField] private float _offest;
        [SerializeField] private float _lookAngle;
        [SerializeField] private float _lookAngleY;

        private Vector3 _targetPosition;
        private DeathTrigger _deathTrigger;

        private void OnEnable()
        {
            _deathTrigger = FindObjectOfType<DeathTrigger>();
            Error.CheckOnNull(_deathTrigger, nameof(DeathTrigger));

            _deathTrigger.PlayerHasLost += Disable;
        }

        private void OnDisable()
        {
            _deathTrigger.PlayerHasLost -= Disable;
        }

        private void LateUpdate()
        {
            _targetPosition = _target.position;
            _targetPosition -= _target.forward * _distance;
            _targetPosition += Vector3.up * _height;
            _targetPosition += _target.right * _offest;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

            var targetRotation = Quaternion.LookRotation(_target.forward, Vector3.up);
            targetRotation.eulerAngles = new Vector3(_lookAngle, targetRotation.eulerAngles.y +_lookAngleY, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void Disable()
        {
            this.enabled = false;
        }
    }
}
