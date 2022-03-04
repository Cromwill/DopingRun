using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem.Model;
using RunnerMovementSystem;

public class RunerPushable : MonoBehaviour
{
    [SerializeField] private MovementSystem _movementSystems;

    private float _pushTime = 0.5f;

    public void Push()
    {
        StartCoroutine(PushAnimation());
    }

    private IEnumerator PushAnimation()
    {
        _movementSystems.SetDirection(Direction.Reverse);

        yield return new WaitForSeconds(_pushTime);

        _movementSystems.SetDirection(Direction.Reverse);
    }
}
