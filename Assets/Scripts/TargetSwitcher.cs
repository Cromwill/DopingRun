using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSwitcher : MonoBehaviour
{
    [SerializeField] private Pushable _pushable;
    [SerializeField] private MoveState _moveState;
    [SerializeField] private SumoFighter _enemy;

    private void OnEnable()
    {
        _pushable.PushEnd += ChangeTarget;
    }

    private void OnDisable()
    {
        _pushable.PushEnd -= ChangeTarget;
    }

    private void ChangeTarget()
    {
        _enemy.FindTarget();
    }
}
