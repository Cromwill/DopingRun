using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Examples;

public class RunerDeathTrigger : LoseTrigger
{
    private CameraFollowing _cameraFollowing;
    private RunerControls _runerControls;
    private CameraTarget _cameraTarget;
    private Collider _other;

    private void Awake()
    {
        _runerControls = new RunerControls();
        _cameraFollowing = FindObjectOfType<CameraFollowing>();
        Error.CheckOnNull(_cameraFollowing, nameof(CameraFollowing));

        _cameraTarget = FindObjectOfType<CameraTarget>();
        Error.CheckOnNull(_cameraTarget, nameof(CameraTarget));
    }

    private void OnEnable()
    {
        YandexSDKIntegration.Instance.Rewarded += Relive;
    }

    private void OnDisable()
    {
        YandexSDKIntegration.Instance.Rewarded -= Relive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _cameraFollowing.enabled = false;
            _runerControls.Disable(player);
            PlayerLost();
            _cameraTarget.gameObject.SetActive(false);

            if (player.TryGetComponent(out DeathHandler deathHandler))
                deathHandler.Die();

            _other = other;
        }

        if (other.TryGetComponent(out CameraTarget cameraTarget))
            cameraTarget.gameObject.SetActive(false);
    }

    private void Relive()
    {
        if (_other != null)
        {
            if (_other.TryGetComponent(out Player player))
            {
                _cameraFollowing.enabled = true;
                _runerControls.Enable(player);
                PlayerRelive();
                _cameraTarget.gameObject.SetActive(true);

                if (player.TryGetComponent(out DeathHandler deathHandler))
                    deathHandler.Relive();
            }

            if (_other.TryGetComponent(out CameraTarget cameraTarget))
                cameraTarget.gameObject.SetActive(true);

            _other = null;
        }
    }
}
