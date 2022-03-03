using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BreakRoad : MonoBehaviour
{
    [SerializeField] private CameraTransition _cameraTransition;

    private Rigidbody _rigidbody;

    private void Awake()
    {
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