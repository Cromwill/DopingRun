using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ILosable
{
    public event Action PlayerHasLost;
    public void PlayerLost();
}
