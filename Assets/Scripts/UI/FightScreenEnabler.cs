using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScreenEnabler : MonoBehaviour
{
    [SerializeField] private FightScreen _fightScreen;

    public void OnSumoFightBegun()
    {
        _fightScreen.gameObject.SetActive(true);
    }
}
