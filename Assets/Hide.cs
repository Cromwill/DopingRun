using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] private GameObject _particls;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Pushable player))
        {
            _particls.SetActive(true);
            _animator.SetBool("Hide", true);
            Debug.Log("11111111111111111111111111");
        }
    }
}
