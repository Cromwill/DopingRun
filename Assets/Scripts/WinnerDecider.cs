using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerDecider : MonoBehaviour
{
    private SumoFighterList _sumoFighterList;
    private DeathTrigger _deathTrigger;

    public event Action Victory;

    private void OnEnable()
    {
        _sumoFighterList = FindObjectOfType<SumoFighterList>();

        Error.CheckOnNull(_sumoFighterList, nameof(SumoFighterList));

        _sumoFighterList.OneFighterLeft += OnLastFighterStand;
    }

    private void OnDisable()
    {
        _sumoFighterList.OneFighterLeft -= OnLastFighterStand;
    }

    private void OnLastFighterStand(SumoFighter fighter)
    {
        if (fighter.TryGetComponent(out CelebrationState celebrationState))
        {
            Victory?.Invoke();
            celebrationState.enabled = true;
        }
    }
}
