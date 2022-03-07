using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnabler : MonoBehaviour
{
    [SerializeField] private PlayerParticleHolder _particleHolder;
    private WinnerDecider _winerDecider;

    private void Awake()
    {
        _winerDecider = FindObjectOfType<WinnerDecider>();
        Error.CheckOnNull(_winerDecider, nameof(WinnerDecider));
    }
    private void Start()
    {
        _particleHolder.gameObject.SetActive(false);
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
        _particleHolder.gameObject.SetActive(true);
    }
}
