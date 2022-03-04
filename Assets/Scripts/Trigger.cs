using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour, ILosable
{
    public event Action PlayerHasLost;

    public void PlayerLost()
    {
        PlayerHasLost?.Invoke();
    }
}
