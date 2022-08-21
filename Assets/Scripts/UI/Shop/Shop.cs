using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<ShirtItem> _shirts;
    [SerializeField] private List<Sprite> _shirtSprites;

    [SerializeField] private List<SyringeItem> _syringeItems;
    [SerializeField] private List<Sprite> _syringeSprites;

    [SerializeField] private List<Item> _shortItems;
    [SerializeField] private List<Sprite> _shortSprites;

    [SerializeField] private Transform _content;
    [SerializeField] private SelectionPanel _selectionPanel;

    private List<ItemView> _itemViews;

    private void Awake()
    {
        _itemViews = new List<ItemView>(_content.GetComponentsInChildren<ItemView>());

        InitShirts();
    }

    private void OnEnable()
    {
        _selectionPanel.SectionChanged += OnSectionChanged;

        foreach (var item in _itemViews)
            item.Clicked += OnItemClick;
    }

    private void OnDisable()
    {
        _selectionPanel.SectionChanged -= OnSectionChanged;

        foreach (var item in _itemViews)
            item.Clicked += OnItemClick;
    }

    private void OnItemClick(Item item, ItemView view)
    {
        if (item.IsBought)
        {
            foreach (var itemView in _itemViews)
                if (itemView.IsBought)
                    itemView.Unequip();

            if (item.TypeOfItem == TypeOfItem.Shirt)
            {
                view.Equip();
                _player.EquipShirt(item);
            }
            else if (item.TypeOfItem == TypeOfItem.Syringe)
            {
                view.Equip();
                _player.EquipSyringe(item);
            }
        }
        else if (_player.Coins >= item.Cost)
        {
            foreach (var itemView in _itemViews)
                if (itemView.IsBought)
                    itemView.Unequip();

            if (item.TypeOfItem == TypeOfItem.Shirt)
                _player.BuyShirt(item);
            else if (item.TypeOfItem == TypeOfItem.Syringe)
                _player.BuySyringe(item);
        }
        else
        {
            Debug.Log("У вас не хватает денег");
        }
    }

    private void OnSectionChanged(SectionView section)
    {
        if (section is SkinSectionView)
        {
            InitShirts();
        }
        else if (section is SyringeSectionView)
        {
            InitSyringes();
        }
        else if (section is ShortSectionView)
        {
            InitShorts();
        }
    }

    private void InitShirts()
    {
        for (int i = 0; i < _shirts.Count; i++)
        {
            _itemViews[i].IsBought = false;

            for (int j = 0; j < _player.PurchasedShirts.Count; j++)
            {
                var shirt = _player.PurchasedShirts[j];

                if (shirt.Shirt == _shirts[i].Shirt)
                {
                    _itemViews[i].Init(shirt, _shirtSprites[i]);
                    break;
                }
            }

            if (_itemViews[i].IsBought == false)
                _itemViews[i].Init(_shirts[i], _shirtSprites[i]);
        }
    }

    private void InitSyringes()
    {
        for (int i = 0; i < _syringeItems.Count; i++)
        {
            _itemViews[i].IsBought = false;

            for (int j = 0; j < _player.PurchasedSyringes.Count; j++)
            {
                var syringe = _player.PurchasedSyringes[j];

                if (syringe.Shirt == _syringeItems[i].Shirt)
                {
                    _itemViews[i].Init(syringe, _syringeSprites[i]);
                    break;
                }
            }

            if (_itemViews[i].IsBought == false)
                _itemViews[i].Init(_syringeItems[i], _syringeSprites[i]);
        }
    }

    private void InitShorts()
    {
        for (int i = 0; i < _shortItems.Count; i++)
        {
            _itemViews[i].IsBought = false;

            for (int j = 0; j < _player.PurchasedShirts.Count; j++)
            {
                var shortItem = _player.PurchasedShirts[j];

                if (shortItem.Shirt == _shortItems[i].Shirt)
                {
                    _itemViews[i].Init(shortItem, _shortSprites[i]);
                    break;
                }
            }

            if (_itemViews[i].IsBought == false)
                _itemViews[i].Init(_shortItems[i], _shortSprites[i]);
        }
    }
}
