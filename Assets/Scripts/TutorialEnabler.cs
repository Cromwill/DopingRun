using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnabler : MonoBehaviour
{
    [SerializeField] private CameraTransition _cameraTransition;
    [SerializeField] private GameObject _tutorialScreen;

    private const string Shown = "Shown";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(Shown))
            gameObject.SetActive(false);
        else
            PlayerPrefs.SetString(Shown, Shown);

        _tutorialScreen.SetActive(false);
    }

    private void OnEnable()
    {
        _cameraTransition.TransitionCompleted += Show;
    }

    private void OnDisable()
    {
        _cameraTransition.TransitionCompleted -= Show;
    }

    public void Close()
    {
        _tutorialScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Show()
    {
        _tutorialScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
