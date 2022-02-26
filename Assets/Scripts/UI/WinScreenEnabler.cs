using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenEnabler : MonoBehaviour
{
    [SerializeField] private float _delay;

    private WinScreen _winScreen;
    private WinerDecider _winerDecider;

    private void OnEnable()
    {
        _winerDecider = FindObjectOfType<WinerDecider>();
        Error.CheckOnNull(_winerDecider, nameof(WinerDecider));

        _winScreen = FindObjectOfType<WinScreen>();
        Error.CheckOnNull(_winScreen, nameof(WinScreen));

        _winScreen.gameObject.SetActive(false);

        _winerDecider.Victory += OnVictory;
    }

    private void OnDisable()
    {
        _winerDecider.Victory -= OnVictory;
    }

    private void OnVictory()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _winScreen.gameObject.SetActive(true);
    }
}
