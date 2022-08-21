using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private GameObject[] _childs;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
#if VK_GAMES
        foreach (var child in _childs)
        {
            child.SetActive(false);
        }

        _image.enabled = false;
#endif
    }
}
