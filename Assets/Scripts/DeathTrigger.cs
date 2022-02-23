using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.TryGetComponent(out EnemyStateMachine stateMachine))
                stateMachine.enabled = false;

            if (enemy.TryGetComponent(out MoveState moveState))
                moveState.enabled = false;

            enemy.OnDying();

            enemy.enabled = false;
        }
    }
}
