using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeCollectionZone : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private Enlargable _enlargable;

    private void Start()
    {
        _enlargable = GetComponentInChildren<Enlargable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Injector injector))
        {
            injector.Disable();
            _enlargable.EnalargeAnimation();
            _particle.Play();
        }
    }
}
