using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour, ILosable
{
    public event Action PlayerHasLost;
    public event Action PlayerHasRelive;

    public void PlayerLost()
    {
        PlayerHasLost?.Invoke();
    }

    public void PlayerRelive()
    {
        PlayerHasRelive?.Invoke();
    }
}
