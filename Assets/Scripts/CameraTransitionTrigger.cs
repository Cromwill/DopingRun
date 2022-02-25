using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraTransition))]
public class CameraTransitionTrigger : MonoBehaviour
{
    private CameraTransition _cameraTransition;

    private void Start()
    {
        _cameraTransition = GetComponent<CameraTransition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _cameraTransition.Transit();
            this.enabled = false;
        }
    }
}
