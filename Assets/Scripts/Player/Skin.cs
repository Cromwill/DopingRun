using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private TypeOfSkin _typeOfShirt;

    public TypeOfSkin TypeOfShirt => _typeOfShirt;
}
