using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : Trigger
{
    private CameraLookAt _cameraLookAt;
    private SumoControls _sumoControls = new SumoControls();

    public event Action<SumoFighter> FighterOffTheRing;

    private void Awake()
    {
        _cameraLookAt = Camera.main.GetComponent<CameraLookAt>();
        Error.CheckOnNull(_cameraLookAt, nameof(CameraLookAt));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out SumoFighter enemy))
        {
            if (enemy.TryGetComponent(out EnemyStateMachine stateMachine))
                stateMachine.enabled = false;

            if (enemy.TryGetComponent(out MoveState moveState))
                moveState.enabled = false;

            if (enemy.TryGetComponent(out PlayerMover playerMover))
                playerMover.enabled = false;

            if (enemy.TryGetComponent(out CelebrationState celebrationState))
                celebrationState.enabled = false;

            enemy.OnDying();

            FighterOffTheRing?.Invoke(enemy);

            enemy.enabled = false;

        }

        if (other.TryGetComponent(out Player player))
        {
            PlayerLost();
            _sumoControls.Disable(player);

            if (enemy.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.constraints = RigidbodyConstraints.None;
                _cameraLookAt.enabled = false;
            }
        }
    }
}
