using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventSystemInstance : MonoBehaviour
{
    private static EventSystemInstance _instance = null;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _audioSource = GetComponent<AudioSource>();
        Screen.fullScreenMode = FullScreenMode.Windowed;
        DontDestroyOnLoad(gameObject);
    }
}
