using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCameraTranstion : CameraTransition
{
    private void Awake()
    {
        _delay = 1f;
        _cameraPoint = FindObjectOfType<VictoryCameraPoint>();
    }
}

