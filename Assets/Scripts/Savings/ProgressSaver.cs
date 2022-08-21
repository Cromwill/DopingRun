using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class ProgressSaver : MonoBehaviour
{
    private const string SaveKey = "Saving";

    public static ProgressSaver Instance = null;

    public event Action<Saving> ProgressLoaded;

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
        if (PlayerPrefs.HasKey(SaveKey))
            OnProgressLoad(PlayerPrefs.GetString(SaveKey));
        else
            ProgressLoaded?.Invoke(new Saving(0, 1, new Item[] { }, new Item[] { }));
    }

    public void Save(Saving saving)
    {
        PlayerPrefs.SetString(SaveKey, GetJsonFromSave(saving));
        PlayerPrefs.Save();
    }

    public void Load(Action<Saving> onLoadCallback)
    {
        string json = "";
        Saving emptySaving = new Saving(0, 1, new Item[] { }, new Item[] { });

        if (PlayerPrefs.HasKey(SaveKey))
            json = PlayerPrefs.GetString(SaveKey);
        else
            json = GetJsonFromSave(emptySaving);

        Saving saving = GetSaveFromJson(json);

        if (saving.Level < 1)
            onLoadCallback(emptySaving);
        else
            onLoadCallback(saving);
    }

    private void OnProgressLoad(string json)
    {
        Saving saving = GetSaveFromJson(json);
        Saving emptySaving = new Saving(0, 1, new Item[] { }, new Item[] { });

        if (saving.Level < 1)
            ProgressLoaded?.Invoke(emptySaving);
        else
            ProgressLoaded?.Invoke(saving);
    }

    private Saving GetSaveFromJson(string json)
    {
        return JsonUtility.FromJson<Saving>(json);
    }

    private string GetJsonFromSave(Saving saving)
    {
        return JsonUtility.ToJson(saving);
    }
}
