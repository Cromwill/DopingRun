using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HidingWallHandler : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private WallButton[] _wallButtons;
    private BreakablePiece[] _breakablePieces;
    private List<Material> _tempMaterials;
    private Material _wallMaterial;

    private void Awake()
    {
        _tempMaterials = _materials.ToList();
        _wallButtons = GetComponentsInChildren<WallButton>();
        _breakablePieces = GetComponentsInChildren<BreakablePiece>();

        InitBreakablePieces();
        InitButtons();
    }

    private void InitBreakablePieces()
    {
        int materialIndex = Random.Range(0, _materials.Length);
        _wallMaterial = _materials[materialIndex];

        foreach (var breakablePiece in _breakablePieces)
        {
            breakablePiece.GetComponent<MeshRenderer>().material = _wallMaterial;
        }
    }

    private void InitButtons()
    {
        foreach (var button in _wallButtons)
        {
            int materialIndex = Random.Range(0, _tempMaterials.Count);

            button.InitButton(_tempMaterials[materialIndex]);

            if (_tempMaterials[materialIndex] == _wallMaterial)
                button.SetCorrect();

            _tempMaterials.RemoveAt(materialIndex);
        }
    }
}
