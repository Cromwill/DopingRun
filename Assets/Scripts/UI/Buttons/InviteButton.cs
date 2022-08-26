using System.Collections;
using System.Collections.Generic;
using Agava.VKGames;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InviteButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
#if YANDEX_GAMES
        gameObject.SetActive(false);
#endif
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
#if VK_GAMES
        SocialInteraction.InviteFriends();
#endif
    }
}
