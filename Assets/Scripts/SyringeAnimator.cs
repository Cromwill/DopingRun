using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeAnimator : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _syringe;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Throw()
    {
        _syringe.enabled = false;

    }

    private void OnInjectionEnd()
    {
        _particleSystem.gameObject.SetActive(false);
    }
}
