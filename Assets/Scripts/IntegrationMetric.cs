using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrationMetric
{

    private const string sessionCount = "sessionCount";
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
        Amplitude.Instance.logEvent("level_complete", CreateLevelEndTimeProperty(levelComplitioTime));

        Amplitude.Instance.logEvent("level_complete", CreateLevelProperty( levelIndex));
    }

    public void OnLevelFail(int levelFailTime, int levelIndex)
    {
        Amplitude.Instance.logEvent("fail", CreateLevelEndTimeProperty(levelFailTime));

        Amplitude.Instance.logEvent("fail", CreateLevelProperty( levelIndex));
    }

    public void OnRestartLevel(int levelIndex)
    {
        Amplitude.Instance.logEvent("restart", CreateLevelProperty(levelIndex));
    }

    private Dictionary<string, object> CreateLevelEndTimeProperty(float levelEndTime)
    {
        Dictionary<string, object> time_spent = new Dictionary<string, object>();
        time_spent.Add("time_spent", levelEndTime);

        return time_spent;
    }

    private Dictionary<string, object> CreateLevelProperty( int levelIndex)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level", levelIndex);

        return level;
    }

    private int CountSession()
    {
        int count = 0;

        if (PlayerPrefs.HasKey(sessionCount))
        {
            count = PlayerPrefs.GetInt(sessionCount);
            count++;
        }

        PlayerPrefs.SetInt(sessionCount, count);

        return count;
    }
}
