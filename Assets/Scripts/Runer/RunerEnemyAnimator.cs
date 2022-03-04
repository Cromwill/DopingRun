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
    private const string _lay = "Lay";

    private void Awake()
    {
        _runerEnemyMover = GetComponent<RunerEnemyMover>();      
    }

    private void OnEnable()
    {
        _runerEnemyMover.Moving += OnMove;
    }

    private void OnDisable()
    {
        _runerEnemyMover.Moving -= OnMove;
    }

    private void OnMove()
    {
        _animator.SetBool(_moving, true);
    }

    public void GrabAnimation()
    {
        _animator.SetTrigger(_grab);
        _animator.SetTrigger(_lay);
    }
}
