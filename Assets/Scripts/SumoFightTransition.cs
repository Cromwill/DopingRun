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

        if (_sumoFighterList == null)
            throw new NullReferenceException($"FindObjectOfType did not find {nameof(SumoFighterList)}");

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
