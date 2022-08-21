using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenEnabler : MonoBehaviour
{
    [SerializeField] private WinnerDecider _wienerDecider;
    [SerializeField] private JoystickCanvas _joystickCanvas;
    [SerializeField] private float _delay;
    [SerializeField] private WinScreen _winScreen;

    private void Awake()
    {
        _winScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _wienerDecider.Victory += OnVictory;
    }

    private void OnDisable()
    {
        _wienerDecider.Victory -= OnVictory;
    }

    private void OnVictory()
    {
        Analytics.Instance.CompleteLevel(LevelsHandler.Instance.Counter);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _winScreen.gameObject.SetActive(true);
        _joystickCanvas.gameObject.SetActive(false);
        _winScreen.ShowLevelNumber();
    }
}
