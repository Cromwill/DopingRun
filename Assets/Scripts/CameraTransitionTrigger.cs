using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionTrigger : MonoBehaviour
{
    private CameraTransition _cameraTransition;

    private void Start()
    {
        _cameraTransition = FindObjectOfType<CameraTransition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            _cameraTransition.Transit();
    }
}
