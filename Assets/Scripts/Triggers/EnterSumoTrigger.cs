using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunerControls))]
public class EnterSumoTrigger : MonoBehaviour
{
    [SerializeField] private SumoFightTransition _sumoFightTransition;
    [SerializeField] private float _transitionSpeed;

    private float _delay;
    private JoystickCanvas _joystickCanvas;
    private SumoControls _sumoControls;
    private RunerControls _runerControls;
    private bool _isPlayerReachedDesitination;
    private bool _isAnimationEnd;

    private void Start()
    {
        _runerControls = new RunerControls();
        _sumoControls = new SumoControls();
        _joystickCanvas = FindObjectOfType<JoystickCanvas>();

        Error.CheckOnNull(_joystickCanvas, nameof(JoystickCanvas));

        StartCoroutine(DelayedCanvasDisable());
    }

    private void OnEnable()
    {
        _sumoFightTransition.PlayerEntered += OnPlayerReachedDestination;
    }

    private void OnDisable()
    {
        _sumoFightTransition.PlayerEntered -= OnPlayerReachedDestination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _runerControls.Disable(player);
            _sumoControls.Enable(player);

            if (player.TryGetComponent(out PlayerMover playerMover))
            {
                StartCoroutine(MoveToFight(playerMover));
            }

            player.GetComponentInChildren<EnlargePlayer>().AnimationEnd += OnAnimationEnd;
        }
    }

    private void OnAnimationEnd(EnlargePlayer enlargePlayer)
    {
        _isAnimationEnd = true;
        enlargePlayer.AnimationEnd -= OnAnimationEnd;
    }

    private IEnumerator MoveToFight(PlayerMover playerMover)
    {

        while (_isAnimationEnd == false)
            yield return null;

        while (_isPlayerReachedDesitination == false)
        {
            Vector3 direction = (_sumoFightTransition.transform.position - playerMover.transform.position).normalized;
            playerMover.Move(direction, _transitionSpeed);

            yield return null;
        }
    }
    private void OnPlayerReachedDestination()
    {
        _sumoControls.EnableJouystick(_joystickCanvas);
        _isPlayerReachedDesitination = true;
    }

    private IEnumerator DelayedCanvasDisable()
    {
        yield return new WaitForSeconds(1f);

        _joystickCanvas.gameObject.SetActive(false);
    }
}
