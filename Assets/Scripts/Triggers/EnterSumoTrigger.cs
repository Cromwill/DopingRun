using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunerControls))]
public class EnterSumoTrigger : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private SumoFightTransition _sumoFightTransition;

    private JoystickCanvas _joystickCanvas;
    private SumoControls _sumoControls;
    private RunerControls _runerControls;
    private bool _isPlayerReachedDesitination;

    private void Start()
    {
        _runerControls = new RunerControls();
        _sumoControls = new SumoControls();
        _joystickCanvas = FindObjectOfType<JoystickCanvas>();

        Error.CheckOnNull(_joystickCanvas, nameof(JoystickCanvas));

        _joystickCanvas.enabled = false;
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
            _runerControls.Disable(player);
            _sumoControls.Enable(player, _joystickCanvas);
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
    private void SetReached()
    {
        _isPlayerReachedDesitination = true;
    }
}
