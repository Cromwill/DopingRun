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
    private void Awake()
    {
        _runerControls = new RunerControls();
        _cameraFollowing = FindObjectOfType<CameraFollowing>();
        Error.CheckOnNull(_cameraFollowing, nameof(CameraFollowing));

        _cameraTarget = FindObjectOfType<CameraTarget>();
        Error.CheckOnNull(_cameraTarget, nameof(CameraTarget));
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
        }

        if (other.TryGetComponent(out CameraTarget cameraTarget))
            cameraTarget.gameObject.SetActive(false);
    }
}
