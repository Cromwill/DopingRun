using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _player.CoinsChanged += OnCoinsChanged;
        _countText.text = _player.Coins.ToString();
    }

    private void OnDisable()
    {
        _player.CoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int amount)
    {
        _countText.text = amount.ToString();
    }
}
