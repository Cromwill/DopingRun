using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDecider : MonoBehaviour
{
    private SumoFighterList _sumoFighterList;
    private DeathTrigger _deathTrigger;

    private void OnEnable()
    {
        _sumoFighterList = FindObjectOfType<SumoFighterList>();
        _sumoFighterList.OneFighterLeft += OnLastFighterStand;
    }

    private void OnDisable()
    {
        _sumoFighterList.OneFighterLeft -= OnLastFighterStand;
    }

    private void OnLastFighterStand(SumoFighter fighter)
    {
        Debug.Log("Victory");

        if (fighter.TryGetComponent(out CelebrationState celebrationState))
            celebrationState.enabled = true;
    }
}
