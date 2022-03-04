using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMuscleCameraTransition : CameraTransition
{
    private void Awake()
    {
        _cameraPoint = FindObjectOfType<GainMuscleCameraPoint>();
    }
}
