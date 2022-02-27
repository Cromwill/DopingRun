using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class SumoRing : MonoBehaviour
{
    [SerializeField] private ShakeData _shakeData;

    private CameraShaking _cameraShaking;
    private CameraShaker _cameraShaker;
    private bool _isShaked;

    private void Start()
    {
        _cameraShaking = FindObjectOfType<CameraShaking>();
        Error.CheckOnNull(_cameraShaking, nameof(CameraShaking));

        _cameraShaker = FindObjectOfType<CameraShaker>();
        Error.CheckOnNull(_cameraShaker, nameof(CameraShaker));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out TargetSwitcher targetSwitcher) && _isShaked == false)
        {
            _cameraShaker.SetShakeTechnique(CameraShaker.ShakeTechniques.LocalSpace);
            _cameraShaking.Shake(_shakeData);

            _isShaked = true;
        }
    }
}
