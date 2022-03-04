using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(MeshRenderer))]
public class WallButton : MonoBehaviour
{
    [SerializeField] private bool _isCorrectButton;
    [SerializeField] private Wall _wall;
    [SerializeField] private Material _correctMaterial;
    [SerializeField] private Material _badMaterial;

    private Animator _animator;
    private const string _pressed = "Pressed";
    private MeshRenderer _meshrenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _meshrenderer = GetComponent<MeshRenderer>();

        if (_isCorrectButton)
            _meshrenderer.material = _correctMaterial;
        else
            _meshrenderer.material = _badMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _animator.SetTrigger(_pressed);

            if(_isCorrectButton)
                _wall.HideWall();
        }
    }
}
