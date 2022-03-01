using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    [SerializeField] private Graber _graber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            _graber.Grab();
    }
}
