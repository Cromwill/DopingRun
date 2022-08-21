using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour, ILosable
{
    public event Action<string> PlayerHasLost;
    public event Action PlayerHasRelive;

    public void PlayerLost(string type)
    {
        PlayerHasLost?.Invoke(type);
    }

    public void PlayerRelive()
    {
        PlayerHasRelive?.Invoke();
    }
}
