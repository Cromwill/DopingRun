using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scriptable Objects/Levels list")]
public class LevelsList : ScriptableObject
{
    [SerializeField] private AssetReference[] _scenes;

    public int SceneCount => _scenes.Length-1;

    public AssetReference GetScene(int index)
    {
        return _scenes[index];
    }

    public AssetReference GetRandomScene(int currentSceneIndex)
    {
        int index =-1;

        if(SceneCount>1)
            while (index != currentSceneIndex)
                index = Random.Range(0, _scenes.Length);

        return _scenes[index];
    }
}
