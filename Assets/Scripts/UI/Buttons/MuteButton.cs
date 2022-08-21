using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _mutedSprite;
    [SerializeField] private Sprite _defaultSprite;

    private static bool _isMuted;

    public static bool IsMuted => _isMuted;

    private void Awake()
    {
        _defaultSprite = _icon.sprite;

        if (_isMuted)
        {
            _isMuted = true;
            _icon.sprite = _mutedSprite;
            AudioListener.volume = _isMuted ? 0 : 1;
        }
    }

    public void OnClick()
    {
        if (_isMuted)
            Unmute();
        else
            Mute();
    }

    public void Mute()
    {
        _isMuted = true;
        _icon.sprite = _mutedSprite;
        AudioListener.volume = _isMuted ? 0 : 1;
    }

    public void Unmute()
    {
        _isMuted = false;
        _icon.sprite = _defaultSprite;
        AudioListener.volume = _isMuted ? 0 : 1;
    }
}
