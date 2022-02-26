using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraLookAtEnabler : MonoBehaviour
{
    [SerializeField] private CameraTransition _CameraTransition;

    private CameraLookAt _cameraLookAt;
    

    private void Start()
    {
        _cameraLookAt = FindObjectOfType<CameraLookAt>();

        Error.CheckOnNull(_cameraLookAt, nameof(CameraLookAt));
    }

    private void OnEnable()
    {
        _CameraTransition.TransitionCompleted += Enable;
    }

    private void OnDisable()
    {
        _CameraTransition.TransitionCompleted -= Enable;
    }
    private void Enable()
    {
        _cameraLookAt.enabled = true;
    }
}
