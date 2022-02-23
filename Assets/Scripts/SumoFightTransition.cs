using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

public class SumoFightTransition : MonoBehaviour
{
    private JoystickCanvas _joystickCanvas;

    private void Start()
    {
        _joystickCanvas = FindObjectOfType<JoystickCanvas>();
        _joystickCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _joystickCanvas.gameObject.SetActive(true);

            if (player.TryGetComponent(out MovementSystem movementSystem))
                movementSystem.enabled = false;

            if (player.TryGetComponent(out MouseInput input))
                input.enabled = false;

            if (player.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.isKinematic = false;

            if (player.TryGetComponent(out PlayerMover playerMover))
                playerMover.enabled = true;
        }
    }
}
