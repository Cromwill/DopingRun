using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SumoFighter))]
public class CelebrationTransition : Transition
{
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void OnVictory()
    {
        NeedTransit = true;
    }
}
