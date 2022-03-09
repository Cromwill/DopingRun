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
    private CameraFollowing _cameraFollowing;
    private float _offset;

    private bool _isMoving;
    private void Awake()
    {
        _cameraFollowing = FindObjectOfType<CameraFollowing>();
        Error.CheckOnNull(_cameraFollowing, nameof(CameraFollowing));

        _cameraFollowing.SetTarget(transform);

        _movementSystem = GetComponent<MovementSystem>();
        Error.CheckOnNull(_movementSystem, nameof(MovementSystem));

        _playerMovementSystem = FindObjectOfType<Player>().GetComponent<MovementSystem>();
        Error.CheckOnNull(_playerMovementSystem, nameof(Player));

        _movementSystem.SetOptions(_playerMovementSystem.Options);
        _movementSystem.SetFirstRoad(_playerMovementSystem.FirstRoad);

        transform.position = _playerMovementSystem.transform.position;
        transform.rotation = _playerMovementSystem.transform.rotation;
    }

    private void OnEnable()
    {
        _playerMovementSystem.TransitedToSegment += OnPlayerTransitToSegment;
    }

    private void OnDisable()
    {
        _playerMovementSystem.TransitedToSegment -= OnPlayerTransitToSegment;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _isMoving = true;

        if (_isMoving)
        {
            _movementSystem.MoveForward();

            _offset = Mathf.Lerp(_movementSystem.Offset, 0, 12f * Time.deltaTime);

            _movementSystem.SetOffset(_offset);
        }
    }

    private void OnPlayerTransitToSegment(TransitionSegment transition)
    {
        _movementSystem.Transit(transition);
    }
}
