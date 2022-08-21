using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoinCollector
{
    public event Action<int> CoinsChanged;

    public int Coins { get; }

    public void Pickup(Coin coin);
}
