using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Saving
{
    [SerializeField] private int _coins;
    [SerializeField] private int _level;
    [SerializeField] private Item[] _shirts;
    [SerializeField] private Item[] _syringes;

    public Saving(int coins, int level, Item[] shirts, Item[] syringes)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));
        if (level < 0)
            throw new ArgumentOutOfRangeException(nameof(level));

        _coins = coins;
        _level = level;
        _shirts = shirts;
        _syringes = syringes;
    }

    public int Coins => _coins;
    public int Level => _level;
    public Item[] Shirts => _shirts;
    public Item[] Syringes => _syringes;
}
