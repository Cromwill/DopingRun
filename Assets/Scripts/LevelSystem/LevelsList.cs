using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scriptable Objects/Levels list")]
public class LevelsList : ScriptableObject
{
    [SerializeField] private AssetReference[] _scenes;

    private AssetReference currentScene;
    public int SceneCount => _scenes.Length-1;

    public AssetReference GetScene(int index)
    {
        currentScene = _scenes[index];
        return currentScene;
    }

    public AssetReference GetCurrentScene()
    {
        return currentScene;
    }

    public AssetReference GetRandomScene(int currentSceneIndex)
    {
        int index = 0;

        if (SceneCount > 1)
        {
            do
            {
                index = Random.Range(0, _scenes.Length);
            } while (index == currentSceneIndex);
        }

        currentScene = _scenes[index];
        return currentScene;
    }
}
