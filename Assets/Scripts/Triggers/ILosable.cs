using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ILosable
{
    public event Action<string> PlayerHasLost;
    public event Action PlayerHasRelive;

    public void PlayerLost(string type);
    public void PlayerRelive();
}
