using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Wall : MonoBehaviour
{
    private Animator _animator;
    private const string _wallDown = "WallDown";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void HideWall()
    {
        _animator.SetTrigger(_wallDown);
    }
}
