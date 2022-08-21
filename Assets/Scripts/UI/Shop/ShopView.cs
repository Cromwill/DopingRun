using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    private void Awake()
    {
        if (Screen.width < Screen.height)
            _gridLayoutGroup.constraintCount = 1;
        else
            _gridLayoutGroup.constraintCount = 2;
    }
}
