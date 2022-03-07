using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeAnimator : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _syringe;

    private void Throw()
    {
        _syringe.enabled = false;
    }
}
