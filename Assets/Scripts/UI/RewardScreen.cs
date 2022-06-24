using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScreen : MonoBehaviour
{
    [SerializeField] private RewardedButton _button;

    private void OnEnable()
    {
        _button.Rewarded += Hide;
    }

    private void OnDisable()
    {
        _button.Rewarded -= Hide;
    }

    public void Show()
    {
        _button.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _button.gameObject.SetActive(false);
    }
}
