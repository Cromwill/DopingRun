using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _isBuyed;

    private Item _template;
    private Button _button;

    public event Action<Item, ItemView> Clicked;

    public bool IsBought { get; set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _template.Bought -= OnItemBuy;
        _template.Equipped -= OnItemEquip;
    }

    public void Init(Item item, Sprite sprite)
    {
        _icon.sprite = sprite;
        _icon.preserveAspect = true;
        _cost.text = item.Cost.ToString();
        _template = item;

        IsBought = _template.IsBought;

        _isBuyed.enabled = _template.IsEquipped;
        _cost.gameObject.SetActive(!_template.IsBought);

        _template.Bought += OnItemBuy;
        _template.Equipped += OnItemEquip;
    }

    public void Unequip()
    {
        _isBuyed.enabled = false;
    }

    public void Equip()
    {
        _isBuyed.enabled = true;
    }

    private void OnClick()
    {
        Clicked?.Invoke(_template, this);
    }

    private void OnItemBuy()
    {
        _cost.gameObject.SetActive(false);
    }

    private void OnItemEquip(bool isEquip)
    {
        _isBuyed.enabled = isEquip;
    }
}
