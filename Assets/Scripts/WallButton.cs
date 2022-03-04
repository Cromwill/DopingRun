using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WallButton : MonoBehaviour
{
    [SerializeField] private Wall _wall;
    [SerializeField] private MeshRenderer _meshrenderer;

    private Animator _animator;
    private const string _pressed = "Pressed";
    private bool _isCorrectButton;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

    public void InitButton(Material material)
    {
        _meshrenderer.material = material;
    }

    public void SetCorrect()
    {
        _isCorrectButton = true;
    }
}
