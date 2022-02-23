using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class CelebrationTransition : Transition
{
    private Enemy _enemy;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.AllEnemyDead += OnVictory;
    }

    private void OnDisable()
    {
        _enemy.AllEnemyDead -= OnVictory;
    }

    public void OnVictory()
    {
        NeedTransit = true;
    }
}
