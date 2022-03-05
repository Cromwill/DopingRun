using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _mesh;

    public void ChangeOutline()
    {
        _mesh.material.SetFloat("_OutlineWidth", 3);
    }
}
