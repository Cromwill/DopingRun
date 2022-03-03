using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IPushable
{
    private BreakablePiece[] _breakablePieces;

    private void Awake()
    {
        _breakablePieces = GetComponentsInChildren<BreakablePiece>();
    }
    public void Push(Vector3 direction, float pushSpeed)
    {
        foreach (var breakablePiece in _breakablePieces)
        {

        }
    }
}
