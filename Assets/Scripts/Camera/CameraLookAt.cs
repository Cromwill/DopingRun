using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    private FocalPoint _focalPoint;

    private void Start()
    {
        _focalPoint = FindObjectOfType<FocalPoint>();
        Error.CheckOnNull(_focalPoint, nameof(FocalPoint));
    }

    private void Update()
    {
        transform.LookAt(_focalPoint.transform);
    }
}
