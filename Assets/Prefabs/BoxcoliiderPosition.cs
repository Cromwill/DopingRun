using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;

public class BoxcoliiderPosition : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    private Player _player;
    private MovementSystem _playerMovementSystem;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        Error.CheckOnNull(_player, nameof(Player));

        _playerMovementSystem = _player.GetComponent<MovementSystem>();
    }
    private void Update()
    {
        _boxCollider.center = new Vector3(_playerMovementSystem.Offset, 0f, 0f);
    }
}
