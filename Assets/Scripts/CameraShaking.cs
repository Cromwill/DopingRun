using System.Collections;
using System.Collections.Generic;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    [SerializeField] private HustleZone _playerHustleZone;

    private CameraShaker _cameraShaker;

    private void Start()
    {
        _cameraShaker = FindObjectOfType<CameraShaker>();
        Error.CheckOnNull(_cameraShaker, nameof(CameraShaker));
    }

    private void OnEnable()
    {
        _playerHustleZone.PlayerCollidedWithPushable += Shake;
    }

    private void OnDisable()
    {
        _playerHustleZone.PlayerCollidedWithPushable -= Shake;
    }

    public void Shake(ShakeData shakeData)
    {
        if (transform.parent == null)
            ShakeWorldSpace(shakeData);
        else
            ShakeLocalSpace(shakeData);
    }

    private void ShakeLocalSpace(ShakeData shakeData)
    {
        if(_cameraShaker.ShakeTechnique == CameraShaker.ShakeTechniques.Matrix)
            _cameraShaker.SetShakeTechnique(CameraShaker.ShakeTechniques.LocalSpace);

        CameraShakerHandler.Shake(shakeData);
    }

    private void ShakeWorldSpace(ShakeData shakeData)
    {
        if (_cameraShaker.ShakeTechnique == CameraShaker.ShakeTechniques.LocalSpace)
            _cameraShaker.SetShakeTechnique(CameraShaker.ShakeTechniques.Matrix);

        CameraShakerHandler.Shake(shakeData);
    }
}
