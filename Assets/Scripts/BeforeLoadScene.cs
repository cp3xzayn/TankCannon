using UnityEngine;

public class BeforeLoadScene
{
    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        var soundManager = GameObject.Instantiate(Resources.Load("SoundSetting"));
        GameObject.DontDestroyOnLoad(soundManager);
    }
}
