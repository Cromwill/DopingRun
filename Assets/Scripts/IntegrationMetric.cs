using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationMetric
{
    private const string sessionCount = "sessionCount";
    public int SessionCount;

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());
        Amplitude.Instance.logEvent("game_start", count);
    }

    public void OnLevelStart(int levelIndex)
    {
        Amplitude.Instance.logEvent("level_start", CreateLevelProperty(levelIndex));
    }

    public void OnLevelComplete(int levelComplitioTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelComplitioTime } };

        Amplitude.Instance.logEvent("level_complete", userInfo);
    }

    public void OnLevelFail(int levelFailTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime } };

        Amplitude.Instance.logEvent("fail", userInfo);
    }

    public void OnRestartLevel(int levelIndex)
    {
        Amplitude.Instance.logEvent("restart", CreateLevelProperty(levelIndex));
    }

    private Dictionary<string, object> CreateLevelProperty( int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(sessionCount))
        {
            count = PlayerPrefs.GetInt(sessionCount);
            count++;
        }

        PlayerPrefs.SetInt(sessionCount, count);
        SessionCount = count;

        return count;
    }
}
