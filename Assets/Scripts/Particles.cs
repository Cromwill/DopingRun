using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Particles : MonoBehaviour
{
    [SerializeField] private HustleZone _hustleZone;

    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
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
