using UnityEngine;

/// <summary>
/// ゲーム起動時に最初に呼び出すクラス
/// </summary>
public class BeforeLoadScene
{
    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        var soundManager = GameObject.Instantiate(Resources.Load("SoundSetting"));
        var sceneLoader = GameObject.Instantiate(Resources.Load("SceneLoad"));
        GameObject.DontDestroyOnLoad(soundManager);
        GameObject.DontDestroyOnLoad(sceneLoader);
    }
}
