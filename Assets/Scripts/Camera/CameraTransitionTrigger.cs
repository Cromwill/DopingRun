using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CameraTransition))]
public class CameraTransitionTrigger : MonoBehaviour
{
    private CameraTransition _cameraTransition;

    private void Start()
    {
        _cameraTransition = GetComponent<CameraTransition>();

        if (_cameraTransition == null)
            throw new NullReferenceException($"FindObjectOfType did not find {nameof(CameraTransition)}");
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
