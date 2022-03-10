using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCameraTranstion : CameraTransition
{
    private void Awake()
    {
        Delay = 1f;
        _cameraPoint = FindObjectOfType<VictoryCameraPoint>();
    }
}

