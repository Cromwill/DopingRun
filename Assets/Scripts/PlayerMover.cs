using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Pushable _pushable;

    private Rigidbody _rigidBody;
    private float _threshold = 0.01f;
    private const int LeftMouseButton = 0;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_pushable.IsPushed)
            return;

        Vector3 direcationForward = Camera.main.transform.forward * _joystick.Vertical;

        Vector3 directioRight = Camera.main.transform.right * _joystick.Horizontal;

        Vector3 direction = (direcationForward + directioRight).normalized;

        direction.y = 0;

        if (Input.GetMouseButton(LeftMouseButton) && direction.magnitude > _threshold)
        {
            Move(direction, _speed);
            Rotate(direction);
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        _rigidBody.MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }
}
