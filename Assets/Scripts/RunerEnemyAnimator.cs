using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunerEnemyMover))]
public class RunerEnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GrabZone _grabZone;

    private RunerEnemyMover _runerEnemyMover;

    private const string _moving = "IsMoving";
    private const string _grab = "Grab";

    private void OnEnable()
    {
        _runerEnemyMover = GetComponent<RunerEnemyMover>();
        _runerEnemyMover.Moving += OnMove;
        _grabZone.PlayerInGrabZone += OnPlayerInGrabZone;
    }

    private void OnDisable()
    {
        _runerEnemyMover.Moving -= OnMove;
        _grabZone.PlayerInGrabZone -= OnPlayerInGrabZone;
    }

    private void OnMove()
    {
        _animator.SetBool(_moving, true);
    }

    private void OnPlayerInGrabZone()
    {
        _animator.SetTrigger(_grab);
    }
}
