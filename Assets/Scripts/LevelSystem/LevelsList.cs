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
        int index = 0;

        int counter = 0;

        if (SceneCount > 1)
        {
            do
            {
                counter++;

                index = Random.Range(0, _scenes.Length);

                if (counter > 5)
                {
                    index = 0;
                    return _scenes[index];
                }

            } while (index != currentSceneIndex);
        }

        return _scenes[index];
    }
}
