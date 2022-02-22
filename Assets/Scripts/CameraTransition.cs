using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Examples;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _timeToTransit;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private CameraFollowing _following;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void Transit()
    {
        _following.enabled = false;
        float distance = Vector3.Distance(transform.position, _target.position);
        StartCoroutine(TransitAnimation(distance));
    }

    private IEnumerator TransitAnimation(float distance)
    {
        float changeSpeed = distance / _timeToTransit;

        while (transform.position!= _target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, changeSpeed * Time.deltaTime);
            transform.LookAt(_player.transform);

            yield return null;
        }

        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation()
    {
        while (transform.rotation != _target.rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _target.rotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
