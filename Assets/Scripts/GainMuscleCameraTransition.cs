using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMuscleCameraTransition : CameraTransition
{
    private void Awake()
    {
        _cameraPoint = FindObjectOfType<GainMuscleCameraPoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CameraTarget cameraTarget))
            cameraTarget.gameObject.SetActive(false);
    }
}
