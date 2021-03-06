using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Examples;
using System;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] protected CameraPoint _cameraPoint;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _setPointAsParent;

    private float _timeToTransit = 1f;
    protected float Delay = 0f;
    private CameraFollowing _following;
    private FocalPoint _focalPoint;
    private Camera _camera;
    private float changeSpeed;
    protected bool LookAtPlayer = true;

    public event Action TransitionCompleted;

    private void Start()
    {
        _camera = Camera.main;

        _focalPoint = FindObjectOfType<Player>().GetComponentInChildren<FocalPoint>();
        Error.CheckOnNull(_focalPoint, nameof(Player));

        _following = FindObjectOfType<CameraFollowing>();
        Error.CheckOnNull(_following, nameof(CameraFollowing));

    }

    public void Transit()
    {
        if (_camera.transform.parent != null)
            _camera.transform.parent = null;

        if (_setPointAsParent)
            _camera.transform.SetParent(_cameraPoint.transform);

        _following.enabled = false;

        float distance = Vector3.Distance(Camera.main.transform.position, _cameraPoint.transform.position);
        changeSpeed = distance / _timeToTransit;
        StartCoroutine(TransitAnimation());
    }

    private IEnumerator TransitAnimation()
    {
        yield return new WaitForSeconds(Delay);

        while (_camera.transform.position != _cameraPoint.transform.position)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPoint.transform.position, changeSpeed * Time.deltaTime);

            _camera.transform.LookAt(_focalPoint.transform);

            yield return null;
        }

        TransitionCompleted?.Invoke();
    }

    private IEnumerator Rotation()
    {
        while (_camera.transform.rotation != _cameraPoint.transform.rotation)
        {
            _camera.transform.rotation = Quaternion.RotateTowards(_camera.transform.rotation, _cameraPoint.transform.rotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
