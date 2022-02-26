using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenEnabler : MonoBehaviour
{
    [SerializeField] private float _delay;

    private DeathTrigger _deathTrigger;
    private LoseScreen _loseScreen;

    private void Start()
    {
        _loseScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _deathTrigger = FindObjectOfType<DeathTrigger>();
        Error.CheckOnNull(_deathTrigger, nameof(DeathTrigger));

        _loseScreen = FindObjectOfType<LoseScreen>();
        Error.CheckOnNull(_loseScreen, nameof(LoseScreen));

        _deathTrigger.PlayerLose += OnLose;
    }

    private void OnDisable()
    {
        _deathTrigger.PlayerLose -= OnLose;
    }

    private void OnLose()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _loseScreen.gameObject.SetActive(true);
    }
}
