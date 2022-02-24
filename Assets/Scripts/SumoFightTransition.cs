using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

public class SumoFightTransition : MonoBehaviour
{
    private JoystickCanvas _joystickCanvas;
    [SerializeField] private Enemy[] _enemys;

    private void Start()
    {
        _enemys = FindObjectsOfType<Enemy>();
        _joystickCanvas = FindObjectOfType<JoystickCanvas>();
        _joystickCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            DisableRunerControls(player);

            EnableSumoControls(player);

            EnableEnemys();
        }
    }

    private void EnableSumoControls(Player player)
    {
        _joystickCanvas.gameObject.SetActive(true);

        if (player.TryGetComponent(out PlayerMover playerMover))
            playerMover.enabled = true;
    }

    private void DisableRunerControls(Player player)
    {
        if (player.TryGetComponent(out MovementSystem movementSystem))
            movementSystem.enabled = false;

        if (player.TryGetComponent(out MouseInput input))
            input.enabled = false;

        if (player.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = false;
    }

    private void EnableEnemys()
    {
        foreach (var tempEnemy in _enemys)
        {
            tempEnemy.enabled = true;

            if (tempEnemy.TryGetComponent(out EnemyStateMachine _machine))
                _machine.enabled = true;

        }
    }
}
