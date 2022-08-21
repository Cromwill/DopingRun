using Agava.YandexGames;
using Agava.VKGames;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    private const string LeaderboardName = "PlaytestBoard";

    [SerializeField] private PanelView _panel;
    [SerializeField] private List<PlayerRecordView> _players;

    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        LevelsHandler.Instance.LevelLoaded += OnLevelLoaded;
        _panel.Opened += OnBoardShow;
    }

    private void OnDisable()
    {
        LevelsHandler.Instance.LevelLoaded -= OnLevelLoaded;
        _panel.Opened -= OnBoardShow;
    }

    public void SetLeaderboardScore(int level)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

#if YANDEX_GAMES
        Leaderboard.SetScore(LeaderboardName, Convert.ToInt32(_player.Coins));
#endif
    }

    private void GetLeaderboardEntries()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
#if YANDEX_GAMES
        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            for (int i = 0; i < result.entries.Length && i < _players.Count; i++)
            {
                LeaderboardEntryResponse entry = result.entries[i];

                string name = entry.player.publicName;
                int score = entry.score;

                if (string.IsNullOrEmpty(name)) name = "No Name";

                _players[i].Init(name, score);
            }
        });
#endif
    }

    private void OnLevelLoaded(int level)
    {
        SetLeaderboardScore(level);
    }

    private void OnBoardShow()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

#if VK_GAMES
        Agava.VKGames.Leaderboard.ShowLeaderboard(_player.Coins);
#endif

#if YANDEX_GAMES
        PlayerAccount.RequestPersonalProfileDataPermission(GetLeaderboardEntries, OnGetProfileDataPermissionError);
#endif
    }

    private void OnGetProfileDataPermissionError(string message)
    {
#if YANDEX_GAMES
        PlayerAccount.Authorize(GetLeaderboardEntries);
#endif
    }
}
