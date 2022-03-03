using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _speed;

    private Rigidbody _rigidBody;
    private float _threshold = 0.01f;
    private const int LeftMouseButton = 0;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

        if(Input.GetMouseButton(LeftMouseButton) && direction.magnitude > _threshold)
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