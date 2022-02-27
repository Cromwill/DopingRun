using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading.Tasks;

public class SumoFighterList : MonoBehaviour
{
    private List<SumoFighter> _fighters;
    private DeathTrigger _deathTrigger;
    private int _delayTime = 2000;

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

    public async void EnableFighters()
    {
        ParachuteFighters();

        await Task.Delay(_delayTime);

        foreach (var fighter in _fighters)
        {
            fighter.enabled = true;

            if (fighter.TryGetComponent(out EnemyStateMachine machine))
                machine.enabled = true;
        }
    }

    private void ParachuteFighters()
    {
        foreach (var fighter in _fighters)
        {
            if (fighter.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.isKinematic = false;
        }
    }

    private void ExcludeFighter(SumoFighter fighter)
    {
        _fighters.Remove(fighter);

        if (_fighters.Count <= 1)
            OneFighterLeft?.Invoke(_fighters[0]);
    }
}
