using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Model;
using System.Threading.Tasks;
using RunnerMovementSystem;

public class RunerPushable : MonoBehaviour
{
    [SerializeField] private MovementSystem _movementSystems;

    private int _pushTime = 500;

    public async void Push()
    {
        _movementSystems.SetDirection(Direction.Reverse);

        await Task.Delay(_pushTime);

        _movementSystems.SetDirection(Direction.Reverse);
    }
}
