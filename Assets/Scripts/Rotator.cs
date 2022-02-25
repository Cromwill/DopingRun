using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _resetSpeed;
    [SerializeField] private float _maxAngle;

    private Quaternion _forwardQuaternionRotation = new Quaternion(0, 0, 0, 1);
    private float xRotation;

    private void Update()
    {
        float pointerX = Input.GetAxis("Mouse X") * _rotationSpeed;

        xRotation += pointerX;

        xRotation = Mathf.Clamp(xRotation, -_maxAngle, _maxAngle);

        if (Input.GetMouseButton(0))
        {
            if (pointerX != 0)
                transform.localRotation = Quaternion.Euler(0, xRotation, 0f);
            else
                ResetRotation();
        }
        else
        {
            ResetRotation();
        }
    }

    private void ResetRotation()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _forwardQuaternionRotation, _resetSpeed * Time.deltaTime);
        xRotation = transform.localRotation.x;
    }
}
