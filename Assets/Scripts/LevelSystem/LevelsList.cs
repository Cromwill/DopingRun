using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scriptable Objects/Levels list")]
public class LevelsList : ScriptableObject
{
    [SerializeField] private Scene[] _scenes;

    private Scene _currentScene;
    public int SceneCount => _scenes.Length - 1;

    public Scene GetScene(int index)
    {
        _currentScene = _scenes[index];
        return _currentScene;
    }

    public Scene GetCurrentScene()
    {
        if (_currentScene == null)
            _currentScene = _scenes[0];

        return _currentScene;
    }

    public Scene GetRandomScene(int currentSceneIndex)
    {
        int index = 0;

        if (SceneCount > 1)
        {
            do
            {
                index = Random.Range(0, _scenes.Length);
            }
            while (index == currentSceneIndex);
        }

        _currentScene = _scenes[index];
        return _currentScene;
    }
}
