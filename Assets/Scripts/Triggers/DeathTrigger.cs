using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public event Action<SumoFighter> FighterOffTheRing;
    public event Action PlayerLose;

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

            enemy.OnDying();

            FighterOffTheRing?.Invoke(enemy);

            enemy.enabled = false;
        }

        if (other.TryGetComponent(out Player player))
            PlayerLose?.Invoke();
    }
}
