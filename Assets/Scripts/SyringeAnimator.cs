using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeAnimator : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _syringes;

    private void Throw()
    {
        foreach (var syringe in _syringes)
        {
            syringe.enabled = false;
        }
    }
}
