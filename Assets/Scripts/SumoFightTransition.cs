using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SumoFightTransition : MonoBehaviour
{
    private SumoFighterList _sumoFighterList;
    private Collider _collider;

    public event Action PlayerEntered;
    private void Start()
    {
        _sumoFighterList = FindObjectOfType<SumoFighterList>();

        Error.CheckOnNull(_sumoFighterList, nameof(SumoFighterList));

        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _sumoFighterList.EnableFighters();
            _collider.enabled = false;
            PlayerEntered?.Invoke();
        }
    }
}
