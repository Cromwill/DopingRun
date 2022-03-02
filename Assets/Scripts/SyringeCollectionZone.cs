using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeCollectionZone : MonoBehaviour
{
    private Enlargable _enlargable;

    private void Start()
    {
        _enlargable = GetComponentInChildren<Enlargable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Injector injector))
        {
            _enlargable.EnalargeAnimation();
        }
    }
}
