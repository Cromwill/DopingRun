using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtEnabler : MonoBehaviour
{
    [SerializeField] private CameraTransition _CameraTransition;

    private CameraLookAt _cameraLookAt;
    

    private void Start()
    {
        _cameraLookAt = FindObjectOfType<CameraLookAt>();
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
