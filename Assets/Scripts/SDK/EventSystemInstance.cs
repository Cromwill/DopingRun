using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemInstance : MonoBehaviour
{
    private static EventSystemInstance _instance = null;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        Screen.fullScreenMode = FullScreenMode.Windowed;
        DontDestroyOnLoad(gameObject);
    }
}
