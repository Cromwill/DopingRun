using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour, IPushable
{
    [SerializeField] private RunerEnemyTrigger _enemyTriggerZone;
    [SerializeField] private RunerEnemyMover _enemyMover;

    public void Push(Vector3 direction)
    {
        _enemyTriggerZone.gameObject.SetActive(false);
        _enemyMover.MoveForward();
    }
}
