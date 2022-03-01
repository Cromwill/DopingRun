using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graber : MonoBehaviour
{
    private RunerEnemyAnimator _runerEnemyAnimator;
    private RunerEnemyMover _runerEnemyMover;

    private void Awake()
    {
        _runerEnemyAnimator = GetComponent<RunerEnemyAnimator>();
        _runerEnemyMover = GetComponent<RunerEnemyMover>();
    }

    public void Grab()
    {
        _runerEnemyAnimator.GrabAnimation();
        _runerEnemyMover.Disable();
    }
}
