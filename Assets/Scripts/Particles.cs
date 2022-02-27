using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Particles : MonoBehaviour
{
    [SerializeField] private HustleZone _hustleZone;

    private ParticleSystem _particles;

    private void OnEnable()
    {
        _particles = GetComponent<ParticleSystem>();
        _hustleZone.CollidedWithPushable += OnCollideWithPushable;
    }

    private void OnDisable()
    {
        _hustleZone.CollidedWithPushable -= OnCollideWithPushable;
    }

    private void OnCollideWithPushable()
    {
        _particles.Play();
    }
}
