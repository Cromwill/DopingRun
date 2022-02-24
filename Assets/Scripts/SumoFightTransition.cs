using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

public class SumoFightTransition : MonoBehaviour
{
    private JoystickCanvas _joystickCanvas;
    private SumoFighterList _sumoFighterList;

    private void Start()
    {
        _sumoFighterList = FindObjectOfType<SumoFighterList>();
        _joystickCanvas = FindObjectOfType<JoystickCanvas>();
        _joystickCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            DisableRunerControls(player);

            EnableSumoControls(player);

            _sumoFighterList.EnableFighters();
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
}
