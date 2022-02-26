using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SumoFighterList : MonoBehaviour
{
    private List<SumoFighter> _fighters;
    private DeathTrigger _deathTrigger;

    public event Action<SumoFighter> OneFighterLeft;

    private void Start()
    {
        _fighters = FindObjectsOfType<SumoFighter>().ToList();
        Error.CheckOnNull(_fighters[0], nameof(SumoFighter));
    }

    private void OnEnable()
    {
        _deathTrigger = FindObjectOfType<DeathTrigger>();
        _deathTrigger.FighterOffTheRing += ExcludeFighter;
    }

    private void OnDisable()
    {
        _deathTrigger.FighterOffTheRing -= ExcludeFighter;
    }

    public void EnableFighters()
    {
        foreach (var fighter in _fighters)
        {
            fighter.enabled = true;

            if (fighter.TryGetComponent(out EnemyStateMachine _machine))
                _machine.enabled = true;
        }
    }

    private void ExcludeFighter(SumoFighter fighter)
    {
        _fighters.Remove(fighter);

        if (_fighters.Count <= 1)
            OneFighterLeft?.Invoke(_fighters[0]);
    }
}
