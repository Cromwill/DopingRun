using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfItem
{
    Shirt,
    Syringe,
    Short
}

[Serializable]
public class Item
{
    [SerializeField] private int _cost;
    [SerializeField] private bool _isBought;
    [SerializeField] private bool _isEquipped;
    [SerializeField] private TypeOfSkin _shirt;
    [SerializeField] private TypeOfItem _typeOfItem;

    public event Action Bought;
    public event Action<bool> Equipped;

    public TypeOfSkin Shirt => _shirt;
    public TypeOfItem TypeOfItem => _typeOfItem;
    public int Cost => _cost;
    public bool IsBought => _isBought;
    public bool IsEquipped => _isEquipped;

    public void Buy()
    {
        _isBought = true;
        Bought?.Invoke();
    }

    public void Equip()
    {
        _isEquipped = true;
        Equipped?.Invoke(IsEquipped);
    }

    public void Unequip()
    {
        _isEquipped = false;
        Equipped?.Invoke(IsEquipped);
    }
}
