using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenEnabler : MonoBehaviour
{
    [SerializeField] private float _delay;

    private WinScreen _winScreen;
    private WinnerDecider _winerDecider;

    private void Awake()
    {
        _winerDecider = FindObjectOfType<WinnerDecider>();
        Error.CheckOnNull(_winerDecider, nameof(WinnerDecider));

        _winScreen = GetComponentInChildren<WinScreen>();
        Error.CheckOnNull(_winScreen, nameof(WinScreen));

        _winScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
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
