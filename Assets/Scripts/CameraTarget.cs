using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;

[RequireComponent(typeof(MovementSystem))]
public class CameraTarget : MonoBehaviour
{
    private MovementSystem _movementSystem;
    private MovementSystem _playerMovementSystem;
    private MouseInput _mouseInput;
    private float _offset;

    private bool _isMoving;
    private void Start()
    {
        _movementSystem = GetComponent<MovementSystem>();

        _playerMovementSystem = FindObjectOfType<Player>().GetComponent<MovementSystem>();

        _movementSystem.SetOptions(_playerMovementSystem.Options);
        _movementSystem.SetFirstRoad(_playerMovementSystem.FirstRoad);

        transform.position = _playerMovementSystem.transform.position;
        transform.rotation = _playerMovementSystem.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _isMoving = true;

        if (_isMoving)
            _movementSystem.MoveForward();

        _offset = Mathf.Lerp(_movementSystem.Offset, 0 , 12f * Time.deltaTime);

        _movementSystem.SetOffset(_offset);
    }
}
