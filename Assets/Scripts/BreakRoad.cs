using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BreakRoad : MonoBehaviour
{
    private CameraTransition _cameraTransition;

    private Rigidbody _rigidbody;

    private const int _breakeableRoadLayer = 7;

    private void Awake()
    {
        _cameraTransition = FindObjectOfType<SumoFightTransition>().GetComponent<CameraTransition>();
        Error.CheckOnNull(_cameraTransition, nameof(SumoFightTransition));

        gameObject.layer = _breakeableRoadLayer;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }
    private void OnEnable()
    {
        
        _cameraTransition.TransitionCompleted += Break;
    }

    private void OnDisable()
    {
        _cameraTransition.TransitionCompleted -= Break;
    }

    private void Break()
    {
        _rigidbody.isKinematic = false;
    }
}
