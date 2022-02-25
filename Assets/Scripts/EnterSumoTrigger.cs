using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

public class EnterSumoTrigger : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private SumoFightTransition _sumoFightTransition;

    private JoystickCanvas _joystickCanvas;
    private bool _isPlayerReachedDesitination;

    private void Start()
    {
        _joystickCanvas = FindObjectOfType<JoystickCanvas>();
        _joystickCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _sumoFightTransition.PlayerEntered += SetReached;
    }

    private void OnDisable()
    {
        _sumoFightTransition.PlayerEntered -= SetReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            DisableRunerControls(player);
            EnableSumoControls(player);
        }

        if(other.TryGetComponent(out PlayerMover playerMover))
        {
            StartCoroutine(MoveToFight(playerMover));
        }
    }

    private IEnumerator MoveToFight(PlayerMover playerMover)
    {
        yield return new WaitForSeconds(_delay);

        Vector3 direction = (_sumoFightTransition.transform.position - playerMover.transform.position).normalized;

        while (_isPlayerReachedDesitination == false)
        {
            playerMover.Move(direction);

            yield return null;
        }
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

    private void EnableSumoControls(Player player)
    {
        _joystickCanvas.gameObject.SetActive(true);

        if (player.TryGetComponent(out PlayerMover playerMover))
            playerMover.enabled = true;
    }

    private void SetReached()
    {
        _isPlayerReachedDesitination = true;
    }
}
