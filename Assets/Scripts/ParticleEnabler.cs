using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnabler : MonoBehaviour
{
    [SerializeField] private PlayerParticleHolder _particleHolder;
    [SerializeField] private CameraTransition _CameraTransition;

    private void Start()
    {
        _particleHolder = FindObjectOfType<PlayerParticleHolder>();
        Error.CheckOnNull(_particleHolder, nameof(PlayerParticleHolder));

        _particleHolder.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _CameraTransition.TransitionCompleted += OnTranstionComplete;
    }

    private void OnDisable()
    {
        _CameraTransition.TransitionCompleted -= OnTranstionComplete;
    }

    private void OnTranstionComplete()
    {
        _particleHolder.gameObject.SetActive(true);
    }
}
