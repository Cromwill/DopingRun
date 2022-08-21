using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Analytics : MonoBehaviour
{
    private const string SessionCountKey = "SessionCount";
    private const string SoftSpentCountKey = "SoftSpentCount";

    private int _sessionCount;
    private int _softSpentCount;
    private int _levelStartTime;

    public static Analytics Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameAnalytics.Initialize();

        if (PlayerPrefs.HasKey(SessionCountKey))
            _sessionCount = PlayerPrefs.GetInt(SessionCountKey);

        _sessionCount++;

        PlayerPrefs.SetInt(SessionCountKey, _sessionCount);
        PlayerPrefs.Save();

        Dictionary<string, object> properties = new Dictionary<string, object>()
        {
            { "count", _sessionCount }
        };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game", properties);
    }

    public void StartLevel(int level)
    {
        _levelStartTime = (int)Time.time;
        Dictionary<string, object> properties = new Dictionary<string, object>()
        {
            { "level", level }
        };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", properties);
    }

    public void CompleteLevel(int level)
    {
        int timeSpent = (int)Time.time - _levelStartTime;
        Dictionary<string, object> properties = new Dictionary<string, object>()
        {
            { "level", level },
            { "time_spent", timeSpent }
        };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level", properties);
    }

    public void FailLevel(int level, string reason)
    {
        int timeSpent = (int)Time.time - _levelStartTime;
        Dictionary<string, object> properties = new Dictionary<string, object>()
        {
            { "level", level },
            { "time_spent", timeSpent },
            { "reason", reason }
        };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level", properties);
    }

    public void RestartLevel(int level)
    {
        Dictionary<string, object> properties = new Dictionary<string, object>()
        {
            { "level", level },
        };

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "LevelRestart", properties);
    }

    public void SoftSpent(string type, string name, int amount)
    {
        if (PlayerPrefs.HasKey(SoftSpentCountKey))
            _softSpentCount = PlayerPrefs.GetInt(SoftSpentCountKey);

        _softSpentCount++;

        PlayerPrefs.SetInt(SoftSpentCountKey, _softSpentCount);
        PlayerPrefs.Save();

        Dictionary<string, object> properties = new Dictionary<string, object>()
        {
            { "type", type },
            { "name", name },
            { "amount", amount },
            { "softSpentCount", _softSpentCount },
        };

        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, name, amount, name, name, properties);
    }

    public void InterstitialStart(string placement, string adSDKname)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, adSDKname, placement);
    }

    public void RewardedShown(string placement, string adSDKname)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.OfferWall, adSDKname, placement);
    }

    public void RewardedStart(string placement, string adSDKname)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, adSDKname, placement);
    }
}
