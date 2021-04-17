using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SceneLoadをするクラス
/// </summary>
public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// スタートボタンが押されたとき
    /// </summary>
    public void OnClickStart()
    {
        FadeManager.Instance.LoadScene("GameScene", 0.3f);
    }

    /// <summary>
    /// タイトルボタンが押されたとき
    /// </summary>
    public void OnClickToTitle()
    {
        FadeManager.Instance.LoadScene("Title", 0.3f);
    }
}
