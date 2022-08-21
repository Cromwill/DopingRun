using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class Player : MonoBehaviour, ICoinCollector
{
    [SerializeField] private List<Skin> _shirts;
    [SerializeField] private List<Skin> _syringes;

    private static int _coins;
    private int _accumulated;

    private static List<Item> _purchasedShirts = new List<Item>();
    private static List<Item> _purchasedSyringes = new List<Item>();

    public event Action<int> CoinsChanged;

    public int Accumulated
    {
        get
        {
            return _accumulated;
        }
        private set
        {
            _accumulated = value;
            CoinsChanged?.Invoke(_coins + _accumulated);
        }
    }

    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;
            CoinsChanged?.Invoke(_coins);
        }
    }

    public List<Item> PurchasedShirts => _purchasedShirts;
    public List<Item> PurchasedSyringes => _purchasedSyringes;

    private void Awake()
    {
        ProgressSaver.Instance.Load(OnLoadCallback);
    }

    private void OnEnable()
    {
        LevelsHandler.Instance.LevelLoaded += OnLevelLoad;
    }

    private void OnDisable()
    {
        LevelsHandler.Instance.LevelLoaded -= OnLevelLoad;
    }

    public void Save()
    {
        int level = LevelsHandler.Instance.Counter;
        ProgressSaver.Instance.Save(new Saving(Coins, level, _purchasedShirts.ToArray(), _purchasedSyringes.ToArray()));
    }

    public void Pickup(Coin coin)
    {
        Accumulated += coin.Value;
    }

    public void BuySyringe(Item item)
    {
        Coins -= item.Cost;

        item.Buy();

        _purchasedSyringes.Add(item);
        EquipSyringe(item);

        Analytics.Instance.SoftSpent("Shop", item.Shirt.ToString(), item.Cost);
    }

    public void BuyShirt(Item item)
    {
        Coins -= item.Cost;

        item.Buy();

        _purchasedShirts.Add(item);
        EquipShirt(item);

        Analytics.Instance.SoftSpent("Shop", item.Shirt.ToString(), item.Cost);
    }

    public void EquipShirt(Item item)
    {
        foreach (var other in _purchasedShirts)
        {
            if (other.Shirt == item.Shirt)
                other.Equip();
            else
                other.Unequip();
        }

        SelectShirt(item);

        int level = LevelsHandler.Instance.Counter;
        ProgressSaver.Instance.Save(new Saving(Coins, level, _purchasedShirts.ToArray(), _purchasedSyringes.ToArray()));
    }

    public void EquipSyringe(Item item)
    {
        foreach (var other in _purchasedSyringes)
        {
            if (other.Shirt == item.Shirt)
                other.Equip();
            else
                other.Unequip();
        }

        SelectSyringe(item);

        int level = LevelsHandler.Instance.Counter;
        ProgressSaver.Instance.Save(new Saving(Coins, level, _purchasedShirts.ToArray(), _purchasedSyringes.ToArray()));
    }

    private void SelectShirt(Item item)
    {
        foreach (var skin in _shirts)
        {
            if (item.Shirt == skin.TypeOfShirt)
                skin.gameObject.SetActive(true);
            else
                skin.gameObject.SetActive(false);
        }
    }

    private void SelectSyringe(Item item)
    {
        foreach (var syringe in _syringes)
        {
            if (item.Shirt == syringe.TypeOfShirt)
                syringe.gameObject.SetActive(true);
            else
                syringe.gameObject.SetActive(false);
        }
    }

    private void OnLoadCallback(Saving saving)
    {
        Coins = saving.Coins;

        _purchasedShirts = new List<Item>(saving.Shirts);
        _purchasedSyringes = new List<Item>(saving.Syringes);

        foreach (var item in _purchasedShirts)
        {
            if (item.IsEquipped)
                EquipShirt(item);
        }

        foreach (var item in _purchasedSyringes)
        {
            if (item.IsEquipped)
                EquipSyringe(item);
        }
    }

    private void OnLevelLoad(int level)
    {
        Coins += Accumulated;

        if (level > 1)
            ProgressSaver.Instance.Save(new Saving(Coins, level, _purchasedShirts.ToArray(), _purchasedSyringes.ToArray()));
    }
}
