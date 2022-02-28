using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZone : MonoBehaviour
{
    public event Action PlayerInGrabZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerInGrabZone?.Invoke();
    }
}
