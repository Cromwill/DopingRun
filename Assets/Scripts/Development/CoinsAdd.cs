using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class CoinsAdd : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
#if VK_GAMES
        Analytics.Instance.RewardedShown("BeforeStart", "VK_games");
#endif
    }

    public void Add()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnRewardedCallback();
        return;
#endif

#if YANDEX_GAMES
        VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback);
#endif

#if VK_GAMES
        void onRewarded()
        {
            OnRewardedCallback();
            OnCloseCallback();
        }

        OnOpenCallback();
        Agava.VKGames.VideoAd.Show(onRewarded);
        Analytics.Instance.RewardedStart("AddCoin", "VK_games");
#endif
    }

    private void OnOpenCallback()
    {
        AudioListener.pause = true;
    }

    private void OnRewardedCallback()
    {
        _player.Coins += 100;
    }

    private void OnCloseCallback()
    {
        AudioListener.pause = false;
    }
}
