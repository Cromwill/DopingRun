using Agava.YandexGames;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    private const string LeaderboardName = "PlaytestBoard";

    [SerializeField] private PanelView _panel;
    [SerializeField] private List<PlayerRecordView> _players;

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
        Leaderboard.SetScore(LeaderboardName, Convert.ToInt32(LevelsHandler.Instance.Counter));
    }

    private void GetLeaderboardEntries()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
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
        PlayerAccount.RequestPersonalProfileDataPermission(GetLeaderboardEntries, OnGetProfileDataPermissionError);
    }

    private void OnGetProfileDataPermissionError(string message)
    {
        PlayerAccount.Authorize(GetLeaderboardEntries);
    }
}
