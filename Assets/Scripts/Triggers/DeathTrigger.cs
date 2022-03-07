using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : Trigger
{
    private float _pushOutForce = 50f;
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
        if (other.TryGetComponent(out DeathHandler deathHandler))
            deathHandler.Die();

        if (other.TryGetComponent(out SumoFighter sumoFighter))
        {
            if (sumoFighter.TryGetComponent(out EnemyStateMachine stateMachine))
                stateMachine.enabled = false;

            if (sumoFighter.TryGetComponent(out MoveState moveState))
                moveState.enabled = false;

            if (sumoFighter.TryGetComponent(out PlayerMover playerMover))
                playerMover.enabled = false;

            if (sumoFighter.TryGetComponent(out CelebrationState celebrationState))
                celebrationState.enabled = false;

            if (sumoFighter.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.constraints = RigidbodyConstraints.None;

            PushOut(sumoFighter);

            sumoFighter.OnDying();

            FighterOffTheRing?.Invoke(sumoFighter);

            sumoFighter.enabled = false;
        }

        if (other.TryGetComponent(out Player player))
        {
            PlayerLost();
            _sumoControls.Disable(player);

            _cameraLookAt.enabled = false;
        }
    }

    private void PushOut(SumoFighter sumoFighter)
    {
        Chest chest = sumoFighter.GetComponentInChildren<Chest>();

        Vector3 direction = sumoFighter.transform.position - transform.position;

        chest.Push(direction.normalized, _pushOutForce);
    }
}
