using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SectionView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _color;

    private Button _button;
    private Color _default;

    public event Action<SectionView> Clicked;
    public event Action Unselected;
    public event Action Selected;

    public bool IsSelected { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _default = _image.color;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void Select()
    {
        _image.color = _color;
        IsSelected = true;

        Selected?.Invoke();
    }

    public void Unselect()
    {
        _image.color = _default;
        IsSelected = false;

        Unselected?.Invoke();
    }

    private void OnClick()
    {
        Clicked?.Invoke(this);
    }
}
